using CleanArchitecture.Application.Interfaces.Services.Auth;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Application.Interfaces.Services.User;
using CleanArchitecture.Infrastructure.Services.Auth;
using CleanArchitecture.Infrastructure.Services.Posts;
using CleanArchitecture.Infrastructure.Services.User;
using Contracts.Common.Interfaces;
using Contracts.Services;
using Infrastructure.Common;
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
                
                .AddTransient<IPostCategoryService, PostCategoryService>()
                .AddTransient<ICurrentUserService, CurrentUserService>();
        }
    }
}