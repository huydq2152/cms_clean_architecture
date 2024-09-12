using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Application.Interfaces.Repositories.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth;
using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Infrastructure.Services.Auth;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> GetRoleByIdAsync(int id)
    {
        var result = await _roleRepository.GetRoleByIdAsync(id);
        return result;
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync(GetAllRolesInput input)
    {
        var result = await _roleRepository.GetAllRolesAsync(input);
        return result;
    }

    public async Task<PagedResult<RoleDto>> GetAllRolesPagedAsync(GetAllRolesInput input)
    {
        var result = await _roleRepository.GetAllRolesPagedAsync(input);
        return result;
    }

    public async Task CreateRoleAsync(CreateRoleDto input)
    {
        await _roleRepository.CreateRoleAsync(input);
    }

    public async Task UpdateRoleAsync(UpdateRoleDto input)
    {
        await _roleRepository.UpdateRoleAsync(input);
    }

    public async Task DeleteRolesAsync(int[] ids)
    {
        await _roleRepository.DeleteRoleAsync(ids);
    }

    public async Task<PermissionDto> GetRolePermissionsAsync(int roleId)
    {
        var result = await _roleRepository.GetRolePermissionsAsync(roleId);
        return result;
    }

    public async Task SaveRolePermissionsAsync(PermissionDto permissionDto)
    {
        await _roleRepository.SaveRolePermissionsAsync(permissionDto);
    }
}