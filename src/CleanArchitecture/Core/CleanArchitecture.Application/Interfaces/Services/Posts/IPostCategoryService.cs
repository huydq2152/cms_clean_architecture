using CleanArchitecture.Application.Dtos.Posts;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Posts;

public interface IPostCategoryService
{
    Task<PostCategoryDto> GetPostCategoryByIdAsync(int id);
    Task<List<PostCategoryDto>> GetAllPostCategoriesAsync();
    Task<PagedResult<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput input);
    Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory);
    Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory);
    Task DeletePostCategoriesAsync(int[] ids);
}