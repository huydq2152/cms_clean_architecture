using CleanArchitecture.Application.Dtos.Auth.Roles;
using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Auth;

public interface IRoleRepository
{
    Task<RoleDto> GetRoleByIdAsync(int id);
    Task<List<RoleDto>> GetAllRolesAsync(GetAllRolesInput input);
    Task<PagedResult<RoleDto>> GetAllRolesPagedAsync(GetAllRolesInput input);
    Task CreateRoleAsync(CreateRoleDto input);
    Task UpdateRoleAsync(UpdateRoleDto input);
    Task DeleteRoleAsync(int[] ids);
    Task<PermissionDto> GetRolePermissionsAsync(int id);
    Task SaveRolePermissionsAsync(PermissionDto permissionDto);
}