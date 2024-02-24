using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.WebAPI.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.SeedWork;

namespace CleanArchitecture.WebAPI.Controllers.Posts;

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

    [HttpPost]
    public async Task<ApiResult<int>> CreatePostCategoryAsync([FromBody] CreatePostCategoryDto postCategory)
    {
        try
        {
            await _postCategoryService.CreatePostCategoryAsync(postCategory);
            return await ApiResult<int>.SuccessAsync("PostCategory created");
        }
        catch (Exception e)
        {
            Log.Error("Error in CreatePostCategoryAsync: {0}", e.Message);
            return await ApiResult<int>.FailureAsync(e.Message);
        }
    }

    [HttpPut]
    public async Task<ApiResult<int>> UpdatePostCategoryAsync([FromBody] UpdatePostCategoryDto postCategory)
    {
        try
        {
            await _postCategoryService.UpdatePostCategoryAsync(postCategory);
            return await ApiResult<int>.SuccessAsync("PostCategory updated");
        }
        catch (Exception e)
        {
            Log.Error("Error in UpdatePostCategoryAsync: {0}", e.Message);
            return await ApiResult<int>.FailureAsync(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ApiResult<int>> DeletePostCategoryAsync(int id)
    {
        try
        {
            await _postCategoryService.DeletePostCategoryAsync(id);
            return await ApiResult<int>.SuccessAsync(id, "PostCategory deleted");
        }
        catch (Exception e)
        {
            Log.Error("Error in DeletePostCategoryAsync: {0}", e.Message);
            return await ApiResult<int>.FailureAsync(e.Message);
        }
    }
}