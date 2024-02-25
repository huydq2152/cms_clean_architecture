using System.Reflection;
using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth;
using CleanArchitecture.Application.Extensions.Auth;
using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.WebAPI.Controllers.Auth
{
    public class RoleController : ApiControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<IActionResult> CreateRole([FromBody] CreateUpdateRoleRequest request)
        {
            await _roleManager.CreateAsync(new AppRole()
            {
                Name = request.Name,
                DisplayName = request.DisplayName,
            });
            return new OkResult();
        }

        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(StaticPermissions.Roles.Edit)]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] CreateUpdateRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();
            role.Name = request.Name;
            role.DisplayName = request.DisplayName;

            await _roleManager.UpdateAsync(role);

            return Ok();
        }

        [HttpDelete]
        [Authorize(StaticPermissions.Roles.Delete)]
        public async Task<IActionResult> DeleteRoles([FromQuery] int[] ids)
        {
            foreach (var id in ids)
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (role == null)
                    return NotFound();
                await _roleManager.DeleteAsync(role);
            }

            return Ok();
        }


        [HttpGet("{id}")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            return Ok(_mapper.Map<AppRole, RoleDto>(role));
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<PagedResult<RoleDto>>> GetRolesAllPaging(string keyword, int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)
                                         || x.DisplayName.Contains(keyword));
            var objQuery = _mapper.ProjectTo<RoleDto>(query);

            var paginationSet = await PagedResult<RoleDto>.ToPagedList(objQuery, pageIndex, pageSize);

            return Ok(paginationSet);
        }

        [HttpGet("all")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
        {
            var model = await _mapper.ProjectTo<RoleDto>(_roleManager.Roles).ToListAsync();
            return Ok(model);
        }

        [HttpGet("{roleId}/permissions")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<PermissionDto>> GetAllRolePermissions(int roleId)
        {
            var model = new PermissionDto();
            var allPermissions = new List<RoleClaimsDto>();
            var types = typeof(StaticPermissions).GetTypeInfo().DeclaredNestedTypes;
            foreach (var type in types)
            {
                allPermissions.GetPermissions(type);
            }

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return NotFound();
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }

            model.RoleClaims = allPermissions;
            return Ok(model);
        }

        [HttpPut("permissions")]
        [Authorize(StaticPermissions.Roles.Edit)]
        public async Task<IActionResult> SavePermission([FromBody] PermissionDto model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            if (role == null)
                return NotFound();

            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }

            return Ok();
        }
    }
}