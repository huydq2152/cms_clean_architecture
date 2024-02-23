using CleanArchitecture.WebAPI.Auth;
using CleanArchitecture.WebAPI.Filter;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddWebApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddControllers();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomOperationIds(description =>
                description.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
            options.SwaggerDoc("AdminAPI", new OpenApiInfo
            {
                Title = "Admin API",
                Version = "v1",
                Description = "API for CleanArchitecture Admin project",
            });
            options.ParameterFilter<SwaggerNullableParameterFilter>();
        });
        services.AddAuthenticationAndAuthorization(configuration);

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration,
        string corsPolicy)
    {
        var allowedOrigins = configuration["AllowedOrigins"];
        if (string.IsNullOrEmpty(allowedOrigins))
            throw new ArgumentNullException("AllowedOrigins is not configured");
        services.AddCors(o => o.AddPolicy(corsPolicy, builder =>
        {
            builder.AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins(allowedOrigins)
                .AllowCredentials();
        }));
        return services;
    }
    
    private static void AddAuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));
        // services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
        // services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
        // services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
    }
}