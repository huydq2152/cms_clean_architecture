using CleanArchitecture.Application.Dtos.Posts;

namespace CleanArchitecture.Application.Interfaces.Services.Posts;

public interface IPostCategoryService
{
    Task<PostCategoryDto> GetPostCategoryByIdAsync(int id);
    Task<IEnumerable<PostCategoryDto>> GetAllPostCategoriesAsync();
    Task<IEnumerable<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query);
    Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory);
    Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory);
    Task DeletePostCategoryAsync(int id);
}