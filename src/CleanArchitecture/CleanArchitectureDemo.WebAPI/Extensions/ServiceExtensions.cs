using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Application.Interfaces.Services.Posts;
using CleanArchitectureDemo.Infrastructure.Repositories;
using CleanArchitectureDemo.Infrastructure.Services.Posts;

namespace CleanArchitectureDemo.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddInfrastructureServices();
        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

        return services;
    }

    private static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPostCategoryRepository, PostCategoryRepository>()
            .AddScoped<IPostCategoryService, PostCategoryService>();
    }
}