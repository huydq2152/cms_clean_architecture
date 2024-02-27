using CleanArchitecture.Domain.Entities.Posts;
using Shared;

namespace CleanArchitecture.Persistence.Contexts.Seed;

public class TestDataForEntitiesCreator
{
    private readonly ApplicationDbContext _dbContext;

    public TestDataForEntitiesCreator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create()
    {
        await CreateTestDataForEntities();
    }

    private async Task CreateTestDataForEntities()
    {
        await CreateTestDataForPostCategory();
    }

    private async Task CreateTestDataForPostCategory()
    {
        var postCategories = new List<PostCategory>
        {
            new()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = "Technology",
                CreationTime = DateTime.Now
            },
            new()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = "Health",
                CreationTime = DateTime.Now
            },
            new()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = "Education",
                CreationTime = DateTime.Now
            }
        };

        if (!_dbContext.PostCategories.Any())
        {
            await _dbContext.PostCategories.AddRangeAsync(postCategories);
            await _dbContext.SaveChangesAsync();
        }
    }
}