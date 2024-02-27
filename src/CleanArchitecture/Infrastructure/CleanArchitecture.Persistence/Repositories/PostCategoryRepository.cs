using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Collection;

namespace CleanArchitecture.Persistence.Repositories;

public class PostCategoryRepository : RepositoryBase<PostCategory, int>, IPostCategoryRepository
{
    private readonly IMapper _mapper;

    public PostCategoryRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper) : base(
        dbContext, unitOfWork)
    {
        _mapper = mapper;
    }

    public async Task<PostCategory> GetPostCategoryByIdAsync(int id)
    {
        var result = await GetByCondition(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        return result;
    }

    public Task<IQueryable<PostCategoryDto>> GetAllPostCategoriesAsync()
    {
        var objQuery = GetByCondition(o => !o.IsDeleted);
        var result = _mapper.ProjectTo<PostCategoryDto>(objQuery);
        return Task.FromResult(result);
    }

    public Task<IQueryable<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput input)
    {
        var objQuery = GetByCondition(o => !o.IsDeleted)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                o => o.Code.Contains(input.Filter) || o.Name.Contains(input.Filter));
        var result = _mapper.ProjectTo<PostCategoryDto>(objQuery);
        return Task.FromResult(result);
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