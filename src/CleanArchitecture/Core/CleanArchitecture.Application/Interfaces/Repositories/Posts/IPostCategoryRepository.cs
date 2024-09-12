using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Posts;

public interface IPostCategoryRepository 
{
    Task<PostCategoryDto> GetPostCategoryByIdAsync(int id);
    Task<List<PostCategoryDto>> GetAllPostCategoriesAsync(GetAllPostCategoriesInput input);
    Task<PagedResult<PostCategoryDto>> GetAllPostCategoriesPagedAsync(GetAllPostCategoriesInput input);
    Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory);
    Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory);
    Task DeletePostCategoryAsync(int id);
    Task<PostCategoryDto> GetPostCategoryBySlug(string slug);
}