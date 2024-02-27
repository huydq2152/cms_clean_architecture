using System.Reflection;
using System.Security.Claims;
using CleanArchitecture.Application.Dtos.Auth;
using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Application.Interfaces.Services.Auth;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.WebAPI.Controllers.Common;
using Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.WebAPI.Controllers.Auth;

public class AuthController : ApiControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IClaimService _claimService;
    private readonly ISerializeService _serializeService;

    public AuthController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService, RoleManager<AppRole> roleManager, IClaimService claimService,
        ISerializeService serializeService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _claimService = claimService;
        _serializeService = serializeService;
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticatedResult>> Login([FromBody] LoginRequest request)
    {
        //Authentication
        if (request == null)
        {
            return BadRequest("Invalid request");
        }

        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null || user.IsActive == false || user.LockoutEnabled)
        {
            return Unauthorized();
        }

        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        //Authorization
        var roles = await _userManager.GetRolesAsync(user);
        var permissions = await this.GetPermissionsByUserIdAsync(user.Id.ToString());
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(UserClaims.Id, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(UserClaims.FirstName, user.FirstName),
            new Claim(UserClaims.Roles, string.Join(";", roles)),
            new Claim(UserClaims.Permissions, _serializeService.Serialize(permissions)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
        await _userManager.UpdateAsync(user);

        return Ok(new AuthenticatedResult()
        {
            Token = accessToken,
            RefreshToken = refreshToken
        });
    }

    private async Task<List<string>> GetPermissionsByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();

        var allPermissions = new List<RoleClaimsDto>();
        if (roles.Contains(StaticRoles.AdminRoleName))
        {
            var types = typeof(StaticPermissions).GetTypeInfo().DeclaredNestedTypes;
            foreach (var type in types)
            {
                _claimService.GetPermissions(allPermissions, type);
            }

            permissions.AddRange(allPermissions.Select(x => x.Value));
        }
        else
        {
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var claims = await _roleManager.GetClaimsAsync(role);
                var roleClaimValues = claims.Select(x => x.Value).ToList();
                permissions.AddRange(roleClaimValues);
            }
        }

        return permissions.Distinct().ToList();
    }
}