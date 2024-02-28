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
        var postCategories = new List<PostCategory>();
        for (int i = 1; i < 36; i++)
        {
            postCategories.Add(new PostCategory()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = $"Name {i}",
                SortOrder = i
            });
        }
        
        if (!_dbContext.PostCategories.Any())
        {
            await _dbContext.PostCategories.AddRangeAsync(postCategories);
            await _dbContext.SaveChangesAsync();
        }
    }
}