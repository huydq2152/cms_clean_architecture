using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.WebAPI.Controllers.Posts;

public class PostCategoryController : ApiControllerBase
{
    private readonly IPostCategoryService _postCategoryService;

    public PostCategoryController(IPostCategoryService postCategoryService)
    {
        _postCategoryService = postCategoryService;
    }

    [HttpGet("{id}")]
    [Authorize(StaticPermissions.PostCategories.View)]
    public async Task<ActionResult<PostCategoryDto>> GetPostCategoryByIdAsync(int id)
    {
        var result = await _postCategoryService.GetPostCategoryByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize(StaticPermissions.PostCategories.View)]
    public async Task<ActionResult<List<PostCategoryDto>>> GetAllPostCategoriesAsync()
    {
        var result = await _postCategoryService.GetAllPostCategoriesAsync();
        return Ok(result);
    }

    [HttpGet("paging")]
    [Authorize(StaticPermissions.PostCategories.View)]
    public async Task<ActionResult<PagedResult<PostCategoryDto>>> GetAllPostCategoryPagedAsync(
        [FromQuery] PostCategoryPagingQueryInput input)
    {
        var result = await _postCategoryService.GetAllPostCategoryPagedAsync(input);
        return Ok(result);
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(StaticPermissions.PostCategories.Create)]
    public async Task<IActionResult> CreatePostCategoryAsync([FromBody] CreatePostCategoryDto input)
    {
        await _postCategoryService.CreatePostCategoryAsync(input);
        return Ok();
    }

    [HttpPut]
    [ValidateModel]
    [Authorize(StaticPermissions.PostCategories.Edit)]
    public async Task<IActionResult> UpdatePostCategoryAsync([FromBody] UpdatePostCategoryDto input)
    {
        await _postCategoryService.UpdatePostCategoryAsync(input);
        return Ok();
    }

    [HttpDelete]
    [Authorize(StaticPermissions.PostCategories.Delete)]
    public async Task<IActionResult> DeletePostCategoryAsync([FromBody] int[] ids)
    {
        await _postCategoryService.DeletePostCategoriesAsync(ids);
        return Ok();
    }
}