using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Common;

public class BlogController: ApiControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    #region Post category

    [HttpGet("all-post-categories")]
    // [Authorize]
    public async Task<ActionResult<List<PostCategoryDto>>> GetAllBlogPostCategoriesAsync([FromQuery] string filter)
    {
        var result = await _blogService.GetBlogAllPostCategoriesAsync(filter);
        return Ok(result);
    }

    [HttpGet("post-category/{id}")]
    // [Authorize]
    public async Task<ActionResult<PostCategoryDto>> GetBlogPostCategoryByIdAsync(int id)
    {
        var result = await _blogService.GetBlogPostCategoryByIdAsync(id);
        return Ok(result);
    }
    #endregion
    
}