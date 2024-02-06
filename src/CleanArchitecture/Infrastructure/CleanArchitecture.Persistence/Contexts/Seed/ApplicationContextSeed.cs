using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.Persistence.Contexts.Seed;

public class ApplicationContextSeed
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;

    public ApplicationContextSeed(ILogger logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
            }
        }
        catch (Exception e)
        {
            _logger.Error("An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Error(e, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        await new RolesAndUsersCreator(_dbContext).Create();
        await new TestDataForEntitiesCreator(_dbContext).Create();
    }
}