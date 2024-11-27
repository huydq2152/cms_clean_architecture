using System.Text;
using CleanArchitecture.Infrastructure.Auth;
using CleanArchitecture.WebAPI.Filter;
using Infrastructure.Configurations;
using Infrastructure.ScheduledJobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

namespace CleanArchitecture.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void AddWebApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();

        services.AddControllers().AddNewtonsoftJson();
        
        services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
            options.MaxRequestBodySize = int.MaxValue;
        });

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();
            options.CustomSchemaIds(type => type.FullName);
            
            options.CustomOperationIds(description =>
                description.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
            options.SwaggerDoc("AdminAPI", new OpenApiInfo
            {
                Title = "Admin API",
                Version = "v1",
                Description = "API for CleanArchitecture Admin project",
            });
            options.ParameterFilter<SwaggerNullableParameterFilter>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        services.AddAuthenticationAndAuthorization(configuration);

        services.AddHangfireServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ValidationModelAttribute<>));
        services.AddScoped(typeof(ValidateMediaTypeAttribute));
    }

    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration,
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
    }

    private static void AddAuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtTokenSettingsSection = configuration.GetSection("JwtTokenSettings");

        services.Configure<JwtTokenSettings>(jwtTokenSettingsSection);

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;

            var jwtTokenSettings = jwtTokenSettingsSection.Get<JwtTokenSettings>();

            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0),
                ValidIssuer = jwtTokenSettings.Issuer,
                ValidAudience = jwtTokenSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.Key))
            };
        });
    }
}