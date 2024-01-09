using CleanArchitectureDemo.Domain.Common;

namespace CleanArchitectureDemo.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<T, K> Repository<T, K>() where T : EntityBase<K>;

        Task<int> SaveChangeAsync();

        Task<int> SaveAndRemoveCache(params string[] cacheKeys);

        Task Rollback();
    }
}