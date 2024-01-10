using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Domain.Entities.Post;
using CleanArchitectureDemo.Infrastructure.Common.Repositories;
using CleanArchitectureDemo.Infrastructure.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Infrastructure.Common.Models;
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

    public async Task<IEnumerable<PostCategory>> GetAllPostCategoriesAsync(bool trackChanges = false, bool isDeleted = false)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PostCategory>> GetAllPostCategoriesAsync()
    {
        var result = await FindAll(false, o => o.IsDeleted.Equals(false)).ToListAsync();
        return result;
    }

    public async Task<PagedList<PostCategory>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query)
    {
        var objQuery =  FindAll(false, o => o.IsDeleted.Equals(false))
            .OrderBy(o => o.Code);
        var result = await PagedList<PostCategory>.ToPagedList(objQuery, query.PageNumber, query.PageSize);
        return result;
    }
}