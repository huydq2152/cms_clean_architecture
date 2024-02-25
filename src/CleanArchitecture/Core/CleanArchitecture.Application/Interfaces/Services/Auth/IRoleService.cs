﻿using CleanArchitecture.Application.Dtos.Auth.Roles;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Auth;

public interface IRoleService
{
    Task<RoleDto> GetRoleByIdAsync(int id);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<PagedResult<RoleDto>> GetAllRolesPagedAsync(RolePagingQueryInput input);
    Task CreateRoleAsync(CreateRoleDto input);
    Task UpdateRoleAsync(UpdateRoleDto input);
    Task DeleteRoleAsync(int[] ids);
    Task<PermissionDto> GetRolePermissionsAsync(int roleId);
    Task SaveRolePermissionsAsync(PermissionDto input);
}