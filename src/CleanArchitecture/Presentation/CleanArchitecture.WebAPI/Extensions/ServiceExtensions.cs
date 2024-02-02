using CleanArchitecture.WebAPI.Filter;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomOperationIds(description =>
                description.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
            options.SwaggerDoc("Admin API", new OpenApiInfo
            {
                Title = "Admin API",
                Version = "v1",
                Description = "API for CleanArchitecture Admin project",
            });
            options.ParameterFilter<SwaggerNullableParameterFilter>();
        });

        return services;
    }
}