using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Common;

public interface IBlogService
{
    Task<PostCategoryDto> GetBlogPostCategoryByIdAsync(int id);
    Task<List<PostCategoryDto>> GetBlogAllPostCategoriesAsync(GetAllPostCategoriesInput query);
    Task<PagedResult<PostCategoryDto>> GetBlogAllPostCategoryPagedAsync(GetAllPostCategoriesInput query);
}