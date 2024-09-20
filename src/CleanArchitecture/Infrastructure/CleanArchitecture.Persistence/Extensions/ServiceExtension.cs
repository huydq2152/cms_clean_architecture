using CleanArchitecture.Application.Interfaces.Repositories.Auth;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Domain.Entities.Auths;
using CleanArchitecture.Persistence.Common;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using CleanArchitecture.Persistence.Contexts.Seed;
using CleanArchitecture.Persistence.Interceptors;
using CleanArchitecture.Persistence.Repositories.Auth;
using CleanArchitecture.Persistence.Repositories.Posts;
using CleanArchitecture.Persistence.Storage;
using Contracts.Common.Interfaces;
using Contracts.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence.Extensions;

public static class ServiceExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddDbContext(configuration);
        services.AddIdentity();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ApplicationContextSeed>()
            .AddScoped<AuditableEntitySaveChangesInterceptor>()
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            
            .AddTransient<IBinaryObjectManager, DbBinaryObjectManager>()
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IPostCategoryRepository, PostCategoryRepository>()
            .AddTransient<IPostRepository, PostRepository>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
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

    private static void AddIdentity(this IServiceCollection services)
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
    }
}