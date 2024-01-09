using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureDemo.Infrastructure.Persistence.Contexts;

public class ApplicationContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public static IConfigurationRoot Configuration { get; set; }
    static ApplicationContextFactory() 
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        Configuration = configuration;
    }
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(new SqlConnection(connectionString));
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}