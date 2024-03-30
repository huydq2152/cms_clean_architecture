using System.Security.Claims;
using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.WebApp.Helper;

public class CustomClaimsPrincipalFactory: UserClaimsPrincipalFactory<AppUser, AppRole>
{
    private readonly UserManager<AppUser> _userManager;
    public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
        _userManager = userManager;
    }
    public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
    {
        var principal = await base.CreateAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        
        ((ClaimsIdentity)principal.Identity)?.AddClaims(new[] {
            new Claim(UserClaims.Id, user.Id.ToString()),
            new Claim(UserClaims.UserName, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(UserClaims.FirstName, user.FirstName),
            new Claim(UserClaims.LastName, user.LastName),
            new Claim(UserClaims.Avatar, string.IsNullOrEmpty(user.Avatar)? string.Empty : user.Avatar),
            new Claim(UserClaims.Roles, string.Join(";",roles)),
        });
        return principal;
    }
}