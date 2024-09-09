using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Infrastructure.Services.Posts;
using CleanArchitecture.Persistence.Common;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using CleanArchitecture.Persistence.Interceptors;
using CleanArchitecture.Persistence.Repositories.Posts;
using CleanArchitecture.WebApp.Helper;
using Contracts.Common.Interfaces;
using Contracts.Common.Interfaces.Repositories;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.WebApp.Extensions;

public static class ServiceExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<AuditableEntitySaveChangesInterceptor>());
            options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });
    }

    public static void AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var systemConfig = configuration.GetSection(nameof(SystemConfig)).Get<SystemConfig>();
        if (systemConfig == null) throw new ArgumentNullException("System config setting is not configured");
        services.AddSingleton(systemConfig);
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = false;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>()
            .AddScoped<UserManager<AppUser>, UserManager<AppUser>>()
            .AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>()
            .AddTransient<IPostCategoryRepository, PostCategoryRepository>()
            .AddTransient<IPostRepository, PostRepository>()
            .AddTransient<IPostCategoryService, PostCategoryService>()
            .AddTransient<IPostService, PostService>();
    }
}