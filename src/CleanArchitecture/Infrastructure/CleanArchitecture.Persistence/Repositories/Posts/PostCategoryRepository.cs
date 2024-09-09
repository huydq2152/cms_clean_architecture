using AutoMapper;
using CleanArchitecture.Application.Common.AutoMappers;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Infrastructure.Common.Helpers.Paging;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Collection;

namespace CleanArchitecture.Persistence.Repositories.Posts;

public class PostCategoryRepository : RepositoryBase<PostCategory, int>, IPostCategoryRepository
{
    public PostCategoryRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : base(
        dbContext, unitOfWork)
    {
    }

    private class QueryInput
    {
        public GetAllPostCategoriesInput? Input { get; init; }
        public int? Id { get; init; }
        public string? Slug { get; init; }
    }

    private IQueryable<PostCategoryDto> PostCategoryQuery(QueryInput queryInput)
    {
        var input = queryInput.Input;
        var id = queryInput.Id;

        var query = GetAll()
            .Where(o => !o.IsDeleted)
            .WhereIf(!string.IsNullOrWhiteSpace(input?.Keyword),
                o => o.Code.Contains(input.Keyword) || o.Name.Contains(input.Keyword))
            .WhereIf(!string.IsNullOrWhiteSpace(queryInput.Slug), o => o.Slug == queryInput.Slug)
            .WhereIf(id.HasValue, e => e.Id == id.Value)
            .WhereIf(input is { ParentId: not null }, e => e.ParentId == input.ParentId)
            .EntityToDtoMapper()
            .ToProjection<PostCategoryDto>();
        return query;
    }

    public async Task<PostCategoryDto> GetPostCategoryByIdAsync(int id)
    {
        var queryInput = new QueryInput { Id = id };
        var objQuery = PostCategoryQuery(queryInput);
        var result = await objQuery.FirstOrDefaultAsync();
        return result;
    }

    public async Task<List<PostCategoryDto>> GetAllPostCategoriesAsync(GetAllPostCategoriesInput input)
    {
        var queryInput = new QueryInput { Input = input };
        var objQuery = PostCategoryQuery(queryInput).Sort("SortOrder, Code");
        var result = await objQuery.ToListAsync();
        return result;
    }

    public async Task<PagedResult<PostCategoryDto>> GetAllPostCategoriesPagedAsync(GetAllPostCategoriesInput input)
    {
        var queryInput = new QueryInput { Input = input };
        var objQuery = PostCategoryQuery(queryInput).Sort("SortOrder, Code");

        var result = await PagedResult<PostCategoryDto>.ToPagedListAsync(objQuery, input.PageIndex, input.PageSize);
        return result;
    }

    public async Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory)
    {
        var entity = postCategory.DtoToEntityMapper().Map<PostCategory>();
        await CreateAsync(entity);
    }

    public async Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory)
    {
        var entity = postCategory.DtoToEntityMapper().Map<PostCategory>();
        await UpdateAsync(entity);
    }

    public async Task DeletePostCategoryAsync(int id)
    {
        var entity = GetByCondition(o => o.Id == id).FirstOrDefault();
        await DeleteAsync(entity);
    }

    public async Task<PostCategoryDto> GetPostCategoryBySlug(string slug)
    {
        var queryInput = new QueryInput()
        {
            Slug = slug
        };
        var objQuery = PostCategoryQuery(queryInput);
        var result = await objQuery.FirstOrDefaultAsync();
        return result;
    }
}