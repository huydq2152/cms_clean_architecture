using CleanArchitecture.Application.Common.AutoMappers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfigurations).Assembly);
    }
}