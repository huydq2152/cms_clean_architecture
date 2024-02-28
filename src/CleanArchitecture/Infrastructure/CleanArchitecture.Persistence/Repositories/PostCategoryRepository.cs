using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Infrastructure.Common.Models.Paging;
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

    private class QueryInput
    {
        public GetAllPostCategoriesInput? Input { get; init; }
        public int? Id { get; init; }
    }

    private IQueryable<PostCategoryDto> PostCategoryQuery(QueryInput queryInput)
    {
        var input = queryInput.Input;
        var id = queryInput.Id;

        var query = from obj in GetAll()
                .Where(o => !o.IsDeleted)
                .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
                .WhereIf(id.HasValue, e => e.Id == id.Value)
            select new PostCategoryDto()
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name,
                Slug = obj.Slug,
                IsActive = obj.IsActive,
                SeoDescription = obj.SeoDescription,
                SortOrder = obj.SortOrder,
                ParentId = obj.ParentId,
                ParentCode = obj.Parent.Code,
                ParentName = obj.Parent.Name
            };
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
        var objQuery = PostCategoryQuery(queryInput).OrderBy(o=>o.SortOrder);
        var result = await objQuery.ToListAsync();
        return result;
    }

    public async Task<PagedResult<PostCategoryDto>> GetAllPostCategoryPagedAsync(GetAllPostCategoriesInput input)
    {
        var queryInput = new QueryInput { Input = input };
        var objQuery = PostCategoryQuery(queryInput).OrderBy(o=>o.SortOrder);

        var result = await PagedResult<PostCategoryDto>.ToPagedListAsync(objQuery, input.PageIndex, input.PageSize);
        return result;
    }

    public async Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory)
    {
        var entity = _mapper.Map<PostCategory>(postCategory);
        await CreateAsync(entity);
    }

    public async Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory)
    {
        var entity = _mapper.Map<PostCategory>(postCategory);
        await UpdateAsync(entity);
    }

    public async Task DeletePostCategoryAsync(int[] ids)
    {
        var entities = await GetAll().Where(o => ids.Contains(o.Id)).ToListAsync();
        await DeleteListAsync(entities);
    }
}