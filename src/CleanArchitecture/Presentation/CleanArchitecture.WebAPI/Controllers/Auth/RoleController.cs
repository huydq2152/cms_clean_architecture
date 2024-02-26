using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Application.Interfaces.Services.Auth;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.WebAPI.Controllers.Auth
{
    public class RoleController : ApiControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id}")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            return Ok(role);
        }

        [HttpGet("all")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<PagedResult<RoleDto>>> GetRolesAllPaging([FromQuery] RolePagingQueryInput input)
        {
            var roles = await _roleService.GetAllRolesPagedAsync(input);

            return Ok(roles);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto input)
        {
            await _roleService.CreateRoleAsync(input);
            return Ok();
        }

        [HttpPut]
        [ValidateModel]
        [Authorize(StaticPermissions.Roles.Edit)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto input)
        {
            await _roleService.UpdateRoleAsync(input);
            return Ok();
        }

        [HttpDelete]
        [Authorize(StaticPermissions.Roles.Delete)]
        public async Task<IActionResult> DeleteRoles([FromQuery] int[] ids)
        {
            await _roleService.DeleteRoleAsync(ids);
            return Ok();
        }

        [HttpGet("{roleId}/permissions")]
        [Authorize(StaticPermissions.Roles.View)]
        public async Task<ActionResult<PermissionDto>> GetAllRolePermissions(int roleId)
        {
            var permissionDto = await _roleService.GetRolePermissionsAsync(roleId);
            return Ok(permissionDto);
        }

        [HttpPut("permissions")]
        [Authorize(StaticPermissions.Roles.Edit)]
        public async Task<IActionResult> SavePermission([FromBody] PermissionDto input)
        {
            await _roleService.SaveRolePermissionsAsync(input);

            return Ok();
        }
    }
}