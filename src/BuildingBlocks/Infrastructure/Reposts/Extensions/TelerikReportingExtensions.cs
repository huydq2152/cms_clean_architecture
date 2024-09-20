using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

namespace Infrastructure.Reposts.Extensions;

public static class TelerikReportingExtensions
{
    public static void AddTelerikReporting(this IServiceCollection services)
    {
        services.TryAddSingleton<IReportServiceConfiguration>(sp =>
            new ReportServiceConfiguration
            {
                HostAppId = $"ReportingCore6App-{Guid.NewGuid()}",
                Storage = new FileStorage(),
                ReportSourceResolver = new UriReportSourceResolver(
                    Path.Combine(sp.GetService<IWebHostEnvironment>()?.ContentRootPath ?? string.Empty, "Reports"))
            });
    }
}