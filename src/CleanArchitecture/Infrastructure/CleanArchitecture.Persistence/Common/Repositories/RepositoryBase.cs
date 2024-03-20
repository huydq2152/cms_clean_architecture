using System.Linq.Expressions;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Contracts.Common.Interfaces.Repositories;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArchitecture.Persistence.Common.Repositories
{
    public class RepositoryBase<T, K> : IRepositoryBase<T, K> where T : EntityBase<K>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryBase(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<T> GetAll(bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> GetAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = GetAll(trackChanges);
            items = includeProperties.Aggregate(items,
                (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges
                ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                : _dbContext.Set<T>().Where(expression);

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var items = GetByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items,
                (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public async Task<T> GetByIdAsync(K id) => await GetByCondition(o => o.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<T> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
            await GetByCondition(o => o.Id.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();

        public K Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            SaveChange();
            return entity.Id;
        }

        public async Task<K> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangeAsync();
            return entity.Id;
        }

        public K CreateAndGetId(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return (K)_dbContext.Entry(entity).Property("Id").CurrentValue;
        }

        public async Task<K> CreateAndGetIdAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return (K)_dbContext.Entry(entity).Property("Id").CurrentValue;
        }

        public IList<K> CreateList(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRangeAsync(entities);
            return entities.Select(o => o.Id).ToList();
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await SaveChangeAsync();
            return entities.Select(o => o.Id).ToList();
        }

        public void Update(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            await SaveChangeAsync();
        }

        public void UpdateList(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Update(entity);
        }

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Update(entity);
            await SaveChangeAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveChangeAsync();
        }

        public void DeleteList(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await SaveChangeAsync();
        }

        public int SaveChange()
        {
            return _unitOfWork.SaveChange();
        }

        public Task<int> SaveChangeAsync()
        {
            return _unitOfWork.SaveChangeAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            var result = await _dbContext.Database.BeginTransactionAsync();
            return result;
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}