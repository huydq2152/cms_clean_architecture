using CleanArchitecture.Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
    }
}