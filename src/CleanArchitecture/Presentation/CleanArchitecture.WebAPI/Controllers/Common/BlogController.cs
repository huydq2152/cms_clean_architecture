using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Services.Common;
using Infrastructure.Common.Helpers.Paging;
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

    #region User

    [HttpGet("user/{id}")]
    public async Task<ActionResult<UserDto>> GetBlogUserByIdAsync(int id)
    {
        var result = await _blogService.GetBlogUserByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("all-users")]
    public async Task<ActionResult<List<UserDto>>> GetAllBlogUsersAsync([FromQuery] GetAllUsersInput input)
    {
        var result = await _blogService.GetAllBlogUsersAsync(input);
        return Ok(result);
    }

    [HttpGet("paging-users")]
    public async Task<ActionResult<PagedResult<UserDto>>> GetAllBlogUsersPagedAsync(
        [FromQuery] GetAllUsersInput input)
    {
        var result = await _blogService.GetAllBlogUsersPagedAsync(input);
        return Ok(result);
    }

    #endregion

    #region PostCategory

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