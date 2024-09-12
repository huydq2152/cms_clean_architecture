using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Domain.StaticData.Auth;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Contracts.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Posts;

public class PostCategoryController : ApiControllerBase
{
    private readonly IPostCategoryService _postCategoryService;

    public PostCategoryController(IPostCategoryService postCategoryService)
    {
        _postCategoryService = postCategoryService;
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [Authorize(StaticPermissions.PostCategories.View)]
    public async Task<ActionResult<PostCategoryDto>> GetPostCategoryByIdAsync(int id)
    {
        var result = await _postCategoryService.GetPostCategoryByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("all")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [Authorize(StaticPermissions.PostCategories.View)]
    public async Task<ActionResult<List<PostCategoryDto>>> GetAllPostCategoriesAsync(
        [FromBody] GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryService.GetAllPostCategoriesAsync(input);
        return Ok(result);
    }

    [HttpPost("paging")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [Authorize(StaticPermissions.PostCategories.View)]
    public async Task<ActionResult<PagedResult<PostCategoryDto>>> GetAllPostCategoryPagedAsync(
        [FromBody] GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryService.GetAllPostCategoryPagedAsync(input);
        return Ok(result);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute<CreatePostCategoryDto>))]
    [Authorize(StaticPermissions.PostCategories.Create)]
    public async Task<IActionResult> CreatePostCategoryAsync([FromBody] CreatePostCategoryDto input)
    {
        await _postCategoryService.CreatePostCategoryAsync(input);
        return Ok();
    }

    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute<UpdatePostCategoryDto>))]
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