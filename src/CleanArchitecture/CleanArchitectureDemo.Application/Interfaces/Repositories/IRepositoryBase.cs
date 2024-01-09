using System.Linq.Expressions;

namespace CleanArchitectureDemo.Application.Interfaces.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges = false);
        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> CreateAsync(T entity);
        Task<IList<int>> CreateListAsync(IList<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateListAsync(IList<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteListAsync(IList<T> entities);
    }
}
