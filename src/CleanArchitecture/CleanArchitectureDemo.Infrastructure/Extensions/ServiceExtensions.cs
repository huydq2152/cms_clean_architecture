using CleanArchitectureDemo.Application.Interfaces.Services.Auth;
using CleanArchitectureDemo.Domain.Entities.Identity;
using CleanArchitectureDemo.Infrastructure.Common;
using CleanArchitectureDemo.Infrastructure.Common.Repositories;
using CleanArchitectureDemo.Infrastructure.Persistence.Contexts;
using CleanArchitectureDemo.Infrastructure.Services.Auth;
using Contracts.Common.Interfaces;
using Contracts.Services;
using Infrastructure.Common;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureDemo.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddDbContext(configuration);
            services.AddIdentity();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<ISerializeService, SerializeService>()
                .AddScoped<ApplicationContextSeed>()
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
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
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        private static void AddAuthenticationAndAuthorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
        }
    }
}