using CleanArchitectureDemo.Application.Interfaces.Repositories;
using CleanArchitectureDemo.Domain.Entities.Post;

namespace CleanArchitectureDemo.Infrastructure.Repositories.Interface;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory, int>
{
    Task<PostCategory> GetPostCategoryByIdAsync(int id);

    Task<IEnumerable<PostCategory>> GetPostCategoriesAsync(bool trackChanges = false,
        bool isDeleted = false);
}