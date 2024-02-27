using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using Contracts.Common.Interfaces.Repositories;

namespace CleanArchitecture.Application.Interfaces.Repositories.Posts;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory, int>
{
    Task<PostCategory> GetPostCategoryByIdAsync(int id);
    Task<IQueryable<PostCategoryDto>> GetAllPostCategoriesAsync();
    Task<IQueryable<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query);
    Task CreatePostCategoryAsync(PostCategory postCategory);
    Task UpdatePostCategoryAsync(PostCategory postCategory);
    Task DeletePostCategoryAsync(PostCategory postCategory);
}