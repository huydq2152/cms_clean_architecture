using CleanArchitecture.Infrastructure.Cache.Configurations;
using CleanArchitecture.Infrastructure.Cache.Storage;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Cache.Extensions;

public static class DistributedCacheServiceExtensions
{
    public static void AddDistributedCache(this IServiceCollection services)
    {
        services.AddServices();
        services.ConfigureRedisCache();
    }
    
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<ITempFileCacheManager, TempFileCacheManager>();
    }
    
    private static void ConfigureRedisCache(this IServiceCollection services)
    {
        var cacheSetting = services.GetOption<RedisCacheSetting>(nameof(RedisCacheSetting));
        if (string.IsNullOrEmpty(cacheSetting.ConnectionString))
        {
            throw new ArgumentNullException("Redis connnection string is not configured");
        }

        services.AddStackExchangeRedisCache(options => { options.Configuration = cacheSetting.ConnectionString; });
    }
}