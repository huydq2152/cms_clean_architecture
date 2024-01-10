using AutoMapper;
using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Application.Interfaces.Services.Posts;
using Infrastructure.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureDemo.Infrastructure.Services.Posts;

public class PostCategoryService : IPostCategoryService
{
    private readonly IPostCategoryRepository _postCategoryRepository;
    private readonly IMapper _mapper;

    public PostCategoryService(IPostCategoryRepository postCategoryRepository, IMapper mapper)
    {
        _postCategoryRepository = postCategoryRepository;
        _mapper = mapper;
    }

    public async Task<IResult> GetPostCategoryByIdAsync(int id)
    {
        var postCategory = await _postCategoryRepository.GetPostCategoryByIdAsync(id);
        var result = _mapper.Map<PostCategoryDto>(postCategory);
        return Results.Ok(result);
    }

    public async Task<IResult> GetAllPostCategoriesAsync()
    {
        var postCategories = await _postCategoryRepository.GetAllPostCategoriesAsync();
        var result = _mapper.Map<List<PostCategoryDto>>(postCategories);
        return Results.Ok(result);
    }

    public async Task<IResult> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query)
    {
        var postCategories = await _postCategoryRepository.GetAllPostCategoryPagedAsync(query);
        var result = _mapper.Map<PagedList<PostCategoryDto>>(postCategories);
        return Results.Ok(result);
    }
}