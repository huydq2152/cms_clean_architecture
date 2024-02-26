using CleanArchitecture.Application.Dtos.Posts;

namespace CleanArchitecture.Application.Interfaces.Services.Posts;

public interface IPostCategoryService
{
    Task<PostCategoryDto> GetPostCategoryByIdAsync(int id);
    Task<IEnumerable<PostCategoryDto>> GetAllPostCategoriesAsync();
    Task<IEnumerable<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput input);
    Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory);
    Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory);
    Task DeletePostCategoriesAsync(int[] ids);
}