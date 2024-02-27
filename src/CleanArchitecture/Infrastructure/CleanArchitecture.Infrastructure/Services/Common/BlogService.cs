using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Common;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Collection;

namespace CleanArchitecture.Infrastructure.Services.Common;

public class BlogService : IBlogService
{
    private readonly IMapper _mapper;
    private readonly IPostCategoryRepository _postCategoryRepository;

    public BlogService(IMapper mapper, IPostCategoryRepository postCategoryRepository)
    {
        _mapper = mapper;
        _postCategoryRepository = postCategoryRepository;
    }

    public async Task<PostCategoryDto> GetBlogPostCategoryByIdAsync(int id)
    {
        var objQuery = await _postCategoryRepository.GetPostCategoryByIdAsync(id);
        var result = _mapper.Map<PostCategoryDto>(objQuery);
        return result;
    }

    public async Task<List<PostCategoryDto>> GetBlogAllPostCategoriesAsync(string filter)
    {
        var objQuery = await _postCategoryRepository.GetAllPostCategoryPagedAsync(new PostCategoryPagingQueryInput()
        {
            Filter = filter
        });
        var result = await objQuery.WhereIf(!string.IsNullOrEmpty(filter),
            o => o.Code.Contains(filter) || o.Name.Contains(filter)).ToListAsync();
        return result;
    }
}