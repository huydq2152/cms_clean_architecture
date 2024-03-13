using System.Linq.Expressions;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;
using Contracts.Common.Interfaces.Repositories;
using Contracts.Domains;
using Contracts.Exceptions;
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

        #region Query

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

        #endregion

        #region Action

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
            if (entity.Id == null)
            {
                throw new BadRequestException("Id is required");
            }

            var exist = _dbContext.Set<T>().Find(entity.Id);
            if (exist == null)
            {
                throw new NotFoundException(nameof(T), entity.Id);
            }

            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            SaveChange();
        }

        public async Task UpdateAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;
            if (entity.Id == null)
            {
                throw new BadRequestException("Id is required");
            }

            var exist = await _dbContext.Set<T>().FindAsync(entity.Id);
            if (exist == null)
            {
                throw new NotFoundException(nameof(T), entity.Id);
            }

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
                await UpdateAsync(entity);
            await SaveChangeAsync();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotFoundException($"{nameof(T)} is not found" ); 
            }
            _dbContext.Set<T>().Remove(entity);
            SaveChangeAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new NotFoundException($"{nameof(T)} is not found" ); 
            }
            _dbContext.Set<T>().Remove(entity);
            await SaveChangeAsync();
        }

        public void DeleteList(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            SaveChange();
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await SaveChangeAsync();
        }
        
        #endregion

        private int SaveChange()
        {
            return _unitOfWork.SaveChange();
        }

        private Task<int> SaveChangeAsync()
        {
            return _unitOfWork.SaveChangeAsync();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }
        
        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }
        
        public Task RollbackTransactionAsync()
        {
            return _dbContext.Database.RollbackTransactionAsync();
        }
    }
}