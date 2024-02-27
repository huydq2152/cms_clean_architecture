using CleanArchitecture.Application.Dtos.Posts;

namespace CleanArchitecture.Application.Interfaces.Services.Common;

public interface IBlogService
{
    Task<PostCategoryDto> GetBlogPostCategoryByIdAsync(int id);
    Task<List<PostCategoryDto>> GetBlogAllPostCategoriesAsync(string filter);
}