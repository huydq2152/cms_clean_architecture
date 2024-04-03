using CleanArchitecture.Domain.Entities.Posts;
using CleanArchitecture.Domain.Enums;
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
        for (var i = 1; i < 15; i++)
        {
            postCategories.Add(new PostCategory()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = $"Danh mục bài viết số {i}",
                SortOrder = i,
                Slug = $"danh-muc-bai-viet-so-{i}",
                SeoDescription = $"Mô tả seo danh mục bài viết số {i}",
                IsActive = true,
            });
        }
        if (!_dbContext.Set<PostCategory>().Any())
        {
            await _dbContext.Set<PostCategory>().AddRangeAsync(postCategories);
        }

        var posts = new List<Post>();
        for (var i = 1; i < 15; i++)
        {
            posts.Add(new Post()
            {
                Code = Helper.StringHelper.ShortIdentity(),
                Name = $"Bài viết số {i}",
                Slug = $"bai-viet-so-{i}",
                Description = $"Mô tả bài viết số {i}",
                Content = $"Nội dung bài viết số {i}",
                SeoDescription = $"Mô tả seo bài viết số {i}",
                CategoryId = i,
                AuthorUserId = 1,
                Status = PostStatusEnum.Published,
                IsActive = true,
            });
        }
        if (!_dbContext.Set<Post>().Any())
        {
            await _dbContext.Set<Post>().AddRangeAsync(posts);
        }

        await _dbContext.SaveChangesAsync();
    }
}