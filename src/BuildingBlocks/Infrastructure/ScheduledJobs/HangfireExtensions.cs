
using Hangfire;
using Hangfire.API.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;

namespace Infrastructure.ScheduledJobs;

public static class HangfireExtensions
{
    public static void AddHangfireServices(this IServiceCollection services)
    {
        var settings = services.GetOption<HangFireSettings>(nameof(HangFireSettings));
        if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            throw new Exception("HangFireSettings is not configured properly!");
        
        services.ConfigureHangfireServices(settings);
        
        services.AddHangfireServer(serverOptions 
            => { serverOptions.ServerName = settings.ServerName; });
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
    
    public static void UseHangfireDashboard(this IApplicationBuilder app, IServiceCollection services)
    {
        var hangfireSettings = services.GetOption<HangFireSettings>(nameof(HangFireSettings));
        var configDashboard = hangfireSettings.Dashboard;
        var hangfireRoute = hangfireSettings.Route;

        app.UseHangfireDashboard(hangfireRoute, new DashboardOptions
        {
            Authorization = new [] { new AuthorizationFilter() },
            DashboardTitle = configDashboard.DashboardTitle,
            StatsPollingInterval = configDashboard.StatsPollingInterval,
            AppPath = configDashboard.AppPath,
            IgnoreAntiforgeryToken = true
        });
    }
}