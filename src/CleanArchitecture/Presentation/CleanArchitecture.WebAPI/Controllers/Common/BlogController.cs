using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Services.Common;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Common;

[Authorize]
public class BlogController : ApiControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    #region Post category

    [HttpGet("post-category/{id}")]
    public async Task<ActionResult<PostCategoryDto>> GetBlogPostCategoryByIdAsync(int id)
    {
        var result = await _blogService.GetBlogPostCategoryByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("all-post-categories")]
    public async Task<ActionResult<List<PostCategoryDto>>> GetAllBlogPostCategoriesAsync(
        [FromQuery] GetAllPostCategoriesInput input)
    {
        var result = await _blogService.GetBlogAllPostCategoriesAsync(input);
        return Ok(result);
    }

    [HttpGet("paging-post-categories")]
    public async Task<ActionResult<PagedResult<PostCategoryDto>>> GetAllBlogPostCategoryPagedAsync(
        [FromQuery] GetAllPostCategoriesInput input)
    {
        var result = await _blogService.GetBlogAllPostCategoryPagedAsync(input);
        return Ok(result);
    }

    #endregion
}