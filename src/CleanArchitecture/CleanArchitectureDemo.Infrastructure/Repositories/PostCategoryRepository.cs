using CleanArchitectureDemo.Application.Interfaces.Repositories;
using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Domain.Entities.Post;
using CleanArchitectureDemo.Infrastructure.Common.Repositories;
using CleanArchitectureDemo.Infrastructure.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureDemo.Infrastructure.Repositories;

public class PostCategoryRepository : RepositoryBase<PostCategory, int>, IPostCategoryRepository
{
    public PostCategoryRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
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