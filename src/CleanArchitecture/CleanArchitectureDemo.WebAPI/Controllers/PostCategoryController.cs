using AutoMapper;
using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Application.Interfaces.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.WebAPI.Controllers;

public class PostCategoryController : ApiControllerBase
{
    private readonly IPostCategoryService _postCategoryService;
    public PostCategoryController(IPostCategoryService postCategoryService)
    {
        _postCategoryService = postCategoryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostCategoryByIdAsync(int id)
    {
        var result = await _postCategoryService.GetPostCategoryByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPostCategoriesAsync()
    {
        var result = await _postCategoryService.GetAllPostCategoriesAsync();
        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPostCategoryPagedAsync([FromQuery] PostCategoryPagingQueryInput query)
    {
        var result = await _postCategoryService.GetAllPostCategoryPagedAsync(query);
        return Ok(result);
    }
}