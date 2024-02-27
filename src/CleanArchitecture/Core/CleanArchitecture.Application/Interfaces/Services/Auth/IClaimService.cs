using CleanArchitecture.Application.Dtos.Auth;
using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Interfaces.Services.Auth;

public interface IClaimService
{
    public void GetPermissions(List<RoleClaimsDto> allPermissions, Type policy);
    public Task AddPermissionClaim(RoleManager<AppRole> roleManager, AppRole role, string permission);
}