using CleanArchitectureDemo.Application.Interfaces.Repositories;
using CleanArchitectureDemo.Domain.Entities.Post;

namespace CleanArchitectureDemo.Persistence.Repositories.Interface;

public interface IPostCategoryRepository : IRepositoryBase<PostCategory>
{
    Task<PostCategory> GetPostCategoryByIdAsync(int id);

    Task<IEnumerable<PostCategory>> GetPostCategoriesAsync(bool trackChanges = false,
        bool isDeleted = false);
}