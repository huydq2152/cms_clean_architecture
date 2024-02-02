using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Domain.Entities.Post;
using Contracts.Common.Interfaces;
using Infrastructure.Common.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PostCategoryRepository : RepositoryBase<PostCategory, int>, IPostCategoryRepository
{
    public PostCategoryRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<PostCategory> GetPostCategoryByIdAsync(int id)
    {
        var result = await GetByCondition(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<PostCategory>> GetAllPostCategoriesAsync()
    {
        var result = await GetByCondition(o => !o.IsDeleted).ToListAsync();
        return result;
    }

    public async Task<PagedList<PostCategory>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query)
    {
        var objQuery = GetByCondition(o => !o.IsDeleted).OrderBy(o => o.Code);
        var result = await PagedList<PostCategory>.ToPagedList(objQuery, query.PageNumber, query.PageSize);
        return result;
    }

    public async Task CreatePostCategoryAsync(PostCategory postCategory)
    {
        await CreateAsync(postCategory);
    }

    public async Task UpdatePostCategoryAsync(PostCategory postCategory)
    {
        await UpdateAsync(postCategory);
    }

    public async Task DeletePostCategoryAsync(PostCategory postCategory)
    {
        await DeleteAsync(postCategory);
    }
}