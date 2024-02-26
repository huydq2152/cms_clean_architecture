using System.Reflection;
using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Application.Extensions.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth;
using CleanArchitecture.Domain.Entities.Identity;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.Infrastructure.Services.Auth;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RoleDto> GetRoleByIdAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role == null)
            throw new Exception("Role not found");

        var result = _mapper.Map<AppRole, RoleDto>(role);
        return result;
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var result = await _mapper.ProjectTo<RoleDto>(_roleManager.Roles).ToListAsync();
        return result;
    }

    public async Task<PagedResult<RoleDto>> GetAllRolesPagedAsync(RolePagingQueryInput input)
    {
        var query = _roleManager.Roles;
        if (!string.IsNullOrEmpty(input.Keyword))
        {
            query = query.Where(x => x.Name.Contains(input.Keyword)
                                     || x.DisplayName.Contains(input.Keyword));
        }

        var objQuery = _mapper.ProjectTo<RoleDto>(query);

        var result = await PagedResult<RoleDto>.ToPagedList(objQuery, input.PageIndex, input.PageSize);
        return result;
    }

    public async Task CreateRoleAsync(CreateRoleDto input)
    {
        await _roleManager.CreateAsync(new AppRole()
        {
            Name = input.Name,
            DisplayName = input.DisplayName,
        });
    }

    public async Task UpdateRoleAsync(UpdateRoleDto input)
    {
        var role = await _roleManager.FindByIdAsync(input.Id.ToString());
        if (role == null)
            throw new Exception("Role not found");
        role.Name = input.Name;
        role.DisplayName = input.DisplayName;

        await _roleManager.UpdateAsync(role);
    }

    public async Task DeleteRolesAsync(int[] ids)
    {
        foreach (var id in ids)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                throw new Exception("Role not found");
            await _roleManager.DeleteAsync(role);
        }
    }

    public async Task<PermissionDto> GetRolePermissionsAsync(int roleId)
    {
        var result = new PermissionDto();
        var allPermissions = new List<RoleClaimsDto>();
        var types = typeof(StaticPermissions).GetTypeInfo().DeclaredNestedTypes;
        foreach (var type in types)
        {
            allPermissions.GetPermissions(type);
        }

        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
            throw new Exception("Role not found");
        result.RoleId = roleId;
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

        result.RoleClaims = allPermissions;
        return result;
    }

    public async Task SaveRolePermissionsAsync(PermissionDto permissionDto)
    {
        var role = await _roleManager.FindByIdAsync(permissionDto.RoleId.ToString());
        if (role == null)
            throw new Exception("Role not found");

        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        var selectedClaims = permissionDto.RoleClaims.Where(a => a.Selected).ToList();
        foreach (var claim in selectedClaims)
        {
            await _roleManager.AddPermissionClaim(role, claim.Value);
        }
    }
}