using CleanArchitecture.Domain.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Persistence.Contexts;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString = ReadDefaultConnectionStringFromAppSettings();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        if(connectionString == null)
            throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        
        optionsBuilder.UseSqlServer(connectionString,
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        optionsBuilder.EnableSensitiveDataLogging();
        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private string? ReadDefaultConnectionStringFromAppSettings()
    {
        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                $"..\\..\\Presentation\\{AppConsts.WebApiProjectName}")))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        return connectionString;
    }
}