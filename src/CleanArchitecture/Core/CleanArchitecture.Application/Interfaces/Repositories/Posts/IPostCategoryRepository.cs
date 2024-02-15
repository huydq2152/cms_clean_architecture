using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Domain.Entities.Post;
using Contracts.Common.Interfaces.Repositories;
using Infrastructure.Common.Models;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Posts;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory, int>
{
    Task<PostCategory> GetPostCategoryByIdAsync(int id);
    Task<IEnumerable<PostCategory>> GetAllPostCategoriesAsync();
    Task<PagedList<PostCategory>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query);
    Task CreatePostCategoryAsync(PostCategory postCategory);
    Task UpdatePostCategoryAsync(PostCategory postCategory);
    Task DeletePostCategoryAsync(PostCategory postCategory);
}