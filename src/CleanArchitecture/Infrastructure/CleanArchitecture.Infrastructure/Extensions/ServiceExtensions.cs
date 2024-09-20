using CleanArchitecture.Application.Interfaces.Services.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using CleanArchitecture.Application.Interfaces.Services.Common;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Infrastructure.Auth;
using CleanArchitecture.Infrastructure.Cache.Extensions;
using CleanArchitecture.Infrastructure.MasstransisRabbitMQ.Extensions;
using CleanArchitecture.Infrastructure.Services.Auth;
using CleanArchitecture.Infrastructure.Services.Auth.User;
using CleanArchitecture.Infrastructure.Services.Common;
using CleanArchitecture.Infrastructure.Services.Posts;
using Contracts.Services;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddConfigurationSettings(configuration);
            services.AddConfigurationMasstransisRabbitMQ(configuration);
            services.AddMediatR();
            services.AddDistributedCache();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
                    
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<ISerializeService, SerializeService>()

                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IClaimService, ClaimService>()

                .AddTransient<ICurrentUserService, CurrentUserService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IUserService, UserService>()
                
                .AddTransient<IPostCategoryService, PostCategoryService>()
                .AddTransient<IBlogService, BlogService>()
                .AddTransient<IPostService, PostService>();
        }
        
        private static void AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSetting = configuration.GetSection(nameof(SmtpEmailSetting)).Get<SmtpEmailSetting>();
            if (emailSetting == null) throw new ArgumentNullException("Smtp email setting is not configured");
            services.AddSingleton(emailSetting);
            
            var uploadImageSettings = configuration.GetSection(nameof(UploadImageSettings)).Get<UploadImageSettings>();
            if (uploadImageSettings == null) throw new ArgumentNullException("UploadImageSettings settings is not configured");
            services.AddSingleton(uploadImageSettings);
        }
    }
}