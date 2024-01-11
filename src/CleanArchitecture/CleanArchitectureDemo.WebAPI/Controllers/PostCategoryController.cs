using AutoMapper;
using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Application.Interfaces.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared;

namespace CleanArchitectureDemo.WebAPI.Controllers;

public class PostCategoryController : ApiControllerBase
{
    private readonly IPostCategoryService _postCategoryService;

    public PostCategoryController(IPostCategoryService postCategoryService)
    {
        _postCategoryService = postCategoryService;
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<PostCategoryDto>> GetPostCategoryByIdAsync(int id)
    {
        try
        {
            var postCategory = await _postCategoryService.GetPostCategoryByIdAsync(id);
            return await ApiResult<PostCategoryDto>.SuccessAsync(postCategory);
        }
        catch (Exception e)
        {
            Log.Error("Error in GetPostCategoryByIdAsync: {0}", e.Message);
            return await ApiResult<PostCategoryDto>.FailureAsync(e.Message);
        }
    }

    [HttpGet]
    public async Task<ApiResult<IEnumerable<PostCategoryDto>>> GetAllPostCategoriesAsync()
    {
        try
        {
            var postCategories = await _postCategoryService.GetAllPostCategoriesAsync();
            return await ApiResult<IEnumerable<PostCategoryDto>>.SuccessAsync(postCategories);
        }
        catch (Exception e)
        {
            Log.Error("Error in GetAllPostCategoriesAsync: {0}", e.Message);
            return await ApiResult<IEnumerable<PostCategoryDto>>.FailureAsync(e.Message);
        }
    }

    [HttpGet("paged")]
    public async Task<ApiResult<IEnumerable<PostCategoryDto>>> GetAllPostCategoryPagedAsync(
        [FromQuery] PostCategoryPagingQueryInput query)
    {
        try
        {
            var postCategories = await _postCategoryService.GetAllPostCategoryPagedAsync(query);
            return await ApiResult<IEnumerable<PostCategoryDto>>.SuccessAsync(postCategories);
        }
        catch (Exception e)
        {
            Log.Error("Error in GetAllPostCategoryPagedAsync: {0}", e.Message);
            return await ApiResult<IEnumerable<PostCategoryDto>>.FailureAsync(e.Message);
        }
    }
}