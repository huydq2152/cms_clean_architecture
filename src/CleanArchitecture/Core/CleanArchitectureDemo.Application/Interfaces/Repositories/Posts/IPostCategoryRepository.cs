using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Domain.Entities.Post;
using Contracts.Common.Interfaces;
using Infrastructure.Common.Models;

namespace CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory, int>
{
    Task<PostCategory> GetPostCategoryByIdAsync(int id);
    Task<IEnumerable<PostCategory>> GetAllPostCategoriesAsync();
    Task<PagedList<PostCategory>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query);
    Task CreatePostCategoryAsync(PostCategory postCategory);
    Task UpdatePostCategoryAsync(PostCategory postCategory);
    Task DeletePostCategoryAsync(PostCategory postCategory);
}