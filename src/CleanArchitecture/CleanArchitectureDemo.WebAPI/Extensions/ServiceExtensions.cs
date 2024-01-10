using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Application.Interfaces.Services.Posts;
using CleanArchitectureDemo.Infrastructure.Repositories;
using CleanArchitectureDemo.Infrastructure.Services.Posts;

namespace CleanArchitectureDemo.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPostCategoryRepository, PostCategoryRepository>()
            .AddScoped<IPostCategoryService, PostCategoryService>();
    }
}