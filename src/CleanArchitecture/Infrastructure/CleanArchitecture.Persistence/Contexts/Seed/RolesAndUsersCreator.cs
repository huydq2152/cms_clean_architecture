using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.Persistence.Contexts.Seed;

public class RolesAndUsersCreator
{
    private readonly ApplicationDbContext _dbContext;

    public RolesAndUsersCreator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create()
    {
        await CreateRolesAndUsers();
    }

    private async Task CreateRolesAndUsers()
    {
        // Admin role 
        var adminRole = await _dbContext.Roles.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Name == StaticRoles.AdminRoleName);
        if (adminRole == null)
        {
            var role = new AppRole()
            {
                Name = StaticRoles.AdminRoleName,
                NormalizedName = StaticRoles.AdminNormalizedName,
                DisplayName = StaticRoles.AdminDisplayName
            };
            adminRole = (await _dbContext.Roles.AddAsync(role)).Entity;
            await _dbContext.SaveChangesAsync();
        }

        // Default users
        var adminUser = await _dbContext.Users.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.UserName == StaticUsers.AdminUserName);
        if (adminUser == null)
        {
            var user = new AppUser()
            {
                FirstName = "Dang",
                LastName = "Huy",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                UserName = StaticUsers.AdminUserName,
                NormalizedUserName = StaticUsers.NormalizedAdminUserName,
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false,
                CreationTime = DateTime.Now
            };
            
            var passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123qwe");

            adminUser = (await _dbContext.Users.AddAsync(user)).Entity;
            await _dbContext.SaveChangesAsync();
            
            //Assign admin role to admin user
            _dbContext.UserRoles.Add(new IdentityUserRole<int>
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}