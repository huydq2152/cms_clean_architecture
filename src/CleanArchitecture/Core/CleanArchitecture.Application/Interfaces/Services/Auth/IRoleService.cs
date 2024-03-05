using CleanArchitecture.Application.Dtos.Auth.Roles;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Auth;

public interface IRoleService
{
    Task<RoleDto> GetRoleByIdAsync(int id);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync(GetAllRolesInput input);
    Task<PagedResult<RoleDto>> GetAllRolesPagedAsync(GetAllRolesInput input);
    Task CreateRoleAsync(CreateRoleDto input);
    Task UpdateRoleAsync(UpdateRoleDto input);
    Task DeleteRolesAsync(int[] ids);
    Task<PermissionDto> GetRolePermissionsAsync(int roleId);
    Task SaveRolePermissionsAsync(PermissionDto input);
}