using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Contracts.Exceptions;
using Infrastructure.Common.Helpers.Paging;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Collection;

namespace CleanArchitecture.Persistence.Repositories.Posts;

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
                .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Keyword),
                    e => e.Code.Contains(input.Keyword) || e.Name.Contains(input.Keyword))
                .WhereIf(id.HasValue, e => e.Id == id.Value)
                .WhereIf(input is { ParentId: not null }, e => e.ParentId == input.ParentId)
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
        var entity = _mapper.Map<PostCategory>(postCategory);
        await CreateAsync(entity);
    }

    public async Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory)
    {
        if (postCategory.Id == null)
        {
            throw new BadRequestException("Id is required");
        }

        var entity = await GetByCondition(o => o.Id == postCategory.Id).FirstOrDefaultAsync();
        if (entity == null)
        {
            throw new NotFoundException(nameof(PostCategory), postCategory.Id);
        }

        entity = _mapper.Map<PostCategory>(postCategory);
        await UpdateAsync(entity);
    }

    public async Task DeletePostCategoryAsync(int id)
    {
        var entity = GetByCondition(o => o.Id == id).FirstOrDefault();
        if (entity == null)
        {
            throw new NotFoundException(nameof(PostCategory), id);
        }

        await DeleteAsync(entity);
    }
}