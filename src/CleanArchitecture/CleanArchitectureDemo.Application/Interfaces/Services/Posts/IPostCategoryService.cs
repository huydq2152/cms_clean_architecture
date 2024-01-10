using Microsoft.AspNetCore.Http;

namespace CleanArchitectureDemo.Application.Interfaces.Services.Posts;

public interface IPostCategoryService
{
    Task<IResult> GetPostCategoryByIdAsync(int id);
    Task<IResult> GetPostCategoriesAsync();
}