using CleanArchitectureDemo.Domain.Entities.Post;
using CleanArchitectureDemo.Infrastructure.Common.Repositories;
using CleanArchitectureDemo.Infrastructure.Persistence.Contexts;
using CleanArchitectureDemo.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureDemo.Persistence.Repositories;

public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
{
    public PostCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PostCategory> GetPostCategoryByIdAsync(int id)
    {
        var result = await FindByCondition(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<PostCategory>> GetPostCategoriesAsync(bool trackChanges = false,
        bool isDeleted = false)
    {
        var result = await FindAll(trackChanges, o => o.IsDeleted.Equals(isDeleted)).ToListAsync();
        return result;
    }
}