using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Common;
using Infrastructure.Common.Models.Paging;
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

    public async Task<List<PostCategoryDto>> GetBlogAllPostCategoriesAsync(GetAllPostCategoriesInput query)
    {
        var result = await _postCategoryRepository.GetAllPostCategoriesAsync(query);
        return result;
    }
    
    public async Task<PagedResult<PostCategoryDto>> GetBlogAllPostCategoryPagedAsync(GetAllPostCategoriesInput query)
    {
        var result = await _postCategoryRepository.GetAllPostCategoryPagedAsync(query);
        return result;
    }
}