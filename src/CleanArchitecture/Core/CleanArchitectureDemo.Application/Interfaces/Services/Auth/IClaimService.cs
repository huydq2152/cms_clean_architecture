using CleanArchitectureDemo.Domain.Entities.Identity;
using Infrastructure.Common.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureDemo.Application.Interfaces.Services.Auth;

public interface IClaimService
{
    public void GetPermissions(List<RoleClaims> allPermissions, Type policy);
    public Task AddPermissionClaim(RoleManager<AppRole> roleManager, AppRole role, string permission);
}