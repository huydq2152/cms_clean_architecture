using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Domain.Entities.Posts;
using Contracts.Common.Interfaces.Repositories;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Posts;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory, int>
{
    Task<PostCategoryDto> GetPostCategoryByIdAsync(int id);
    Task<List<PostCategoryDto>> GetAllPostCategoriesAsync(GetAllPostCategoriesInput input);
    Task<PagedResult<PostCategoryDto>> GetAllPostCategoryPagedAsync(GetAllPostCategoriesInput input);
    Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory);
    Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory);
    Task DeletePostCategoryAsync(int[] ids);
}