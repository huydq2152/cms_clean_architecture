using CleanArchitectureDemo.Domain.Entities.Post;
using Contracts.Common.Interfaces;

namespace CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory, int>
{
    Task<PostCategory> GetPostCategoryByIdAsync(int id);

    Task<IEnumerable<PostCategory>> GetPostCategoriesAsync(bool trackChanges = false,
        bool isDeleted = false);
}