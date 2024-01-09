using CleanArchitectureDemo.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitectureDemo.Infrastructure.Persistence.Contexts;

public class ApplicationContextSeed
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public ApplicationContextSeed(ILogger logger, ApplicationDbContext dbContext, UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
            }
        }
        catch (Exception e)
        {
            _logger.Error("An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Error(e, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default roles
        var adminRole = new AppRole()
        {
            Name = "Admin",
            DisplayName = "Admin"
        };

        if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
        {
            await _roleManager.CreateAsync(adminRole);
        }

        // Default users
        var admin = new AppUser()
        {
            FirstName = "Huy",
            LastName = "Dang", 
            IsActive = true, 
            UserName = "admin", 
            Email = "admin@gmail.com"
        };

        if (_userManager.Users.All(u => u.UserName != admin.UserName))
        {
            await _userManager.CreateAsync(admin, "123qwe");
            await _userManager.AddToRolesAsync(admin, new[] { adminRole.Name });
        }
    }
}