using Contracts.Common.Interfaces.Repositories;
using Contracts.Domains;

namespace Contracts.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<T, K> Repository<T, K>() where T : EntityBase<K>;
        int SaveChange();
        Task<int> SaveChangeAsync();
    }
}