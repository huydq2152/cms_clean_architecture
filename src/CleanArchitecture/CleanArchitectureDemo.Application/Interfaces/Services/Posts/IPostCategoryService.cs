using CleanArchitectureDemo.Application.Dtos.Posts;
using Microsoft.AspNetCore.Http;
using Shared.SeedWork;

namespace CleanArchitectureDemo.Application.Interfaces.Services.Posts;

public interface IPostCategoryService
{
    Task<IResult> GetPostCategoryByIdAsync(int id);
    Task<IResult> GetAllPostCategoriesAsync();
    Task<IResult> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query);
}