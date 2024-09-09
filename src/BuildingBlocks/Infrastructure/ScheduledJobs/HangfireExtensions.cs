
using Hangfire;
using Hangfire.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;
using Shared.Extensions;

namespace Infrastructure.ScheduledJobs;

public static class HangfireExtensions
{
    public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("HangFireSettings").Get<HangFireSettings>();
        if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            throw new Exception("HangFireSettings is not configured properly!");
        
        services.ConfigureHangfireServices(settings);
        
        services.AddHangfireServer(serverOptions 
            => { serverOptions.ServerName = settings.ServerName; });
        
        return services;
    }

    private static void ConfigureHangfireServices(this IServiceCollection services,
        HangFireSettings settings)
    {
        services.AddHangfire(cfg => cfg
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(settings.ConnectionString));
    }
    
    internal static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration configuration)
    {
        var configDashboard = configuration.GetSection("HangFireSettings:Dashboard").Get<DashboardOptions>();
        var hangfireSettings = configuration.GetSection("HangFireSettings").Get<HangFireSettings>();
        var hangfireRoute = hangfireSettings.Route;

        app.UseHangfireDashboard(hangfireRoute, new DashboardOptions
        {
            Authorization = new [] { new AuthorizationFilter() },
            DashboardTitle = configDashboard.DashboardTitle,
            StatsPollingInterval = configDashboard.StatsPollingInterval,
            AppPath = configDashboard.AppPath,
            IgnoreAntiforgeryToken = true
        });

        return app;
    }
}