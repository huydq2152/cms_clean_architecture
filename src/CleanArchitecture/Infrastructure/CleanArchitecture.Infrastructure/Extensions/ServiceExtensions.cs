using CleanArchitecture.Application.Interfaces.Services.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Infrastructure.Services.Auth;
using CleanArchitecture.Infrastructure.Services.Auth.User;
using CleanArchitecture.Infrastructure.Services.Posts;
using Contracts.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<ISerializeService, SerializeService>()

                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IClaimService, ClaimService>()

                .AddTransient<ICurrentUserService, CurrentUserService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IPostCategoryService, PostCategoryService>();

        }
    }
}