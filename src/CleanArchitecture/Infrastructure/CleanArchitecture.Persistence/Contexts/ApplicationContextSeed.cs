using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Domain.Entities.Post;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared;

namespace CleanArchitecture.Persistence.Contexts;

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
            NormalizedName = "ADMIN",
            DisplayName = "Quản trị viên"
        };

        if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
        {
            await _roleManager.CreateAsync(adminRole);
        }

        // Default users
        var admin = new AppUser()
        {
            FirstName = "Dang",
            LastName = "Huy",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            IsActive = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = false,
            CreationTime = DateTime.Now
        };
        var passwordHasher = new PasswordHasher<AppUser>();
        admin.PasswordHash = passwordHasher.HashPassword(admin, "123qwe");

        if (_userManager.Users.All(u => u.UserName != admin.UserName))
        {
            await _userManager.CreateAsync(admin, "123qwe");
            await _userManager.AddToRolesAsync(admin, new[] { adminRole.Name });
        }
        
        // Default post categories
        var postCategories = new List<PostCategory>
        {
            new()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = "Technology",
                CreationTime = DateTime.Now
            },
            new()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = "Health",
                CreationTime = DateTime.Now
            },
            new()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = "Education",
                CreationTime = DateTime.Now
            }
        };
        
        if(!_dbContext.PostCategories.Any())
        {
            await _dbContext.PostCategories.AddRangeAsync(postCategories);
        }
    }
}