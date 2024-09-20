using CleanArchitecture.Domain.Entities.Apps;
using CleanArchitecture.Persistence.Common.Repositories;
using CleanArchitecture.Persistence.Contexts;
using Contracts.Common.Interfaces;

namespace CleanArchitecture.Persistence.Storage;

public class DbBinaryObjectManager : RepositoryBase<BinaryObject, Guid>, IBinaryObjectManager
{
    public DbBinaryObjectManager(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<BinaryObject> GetOrNullAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    public async Task CreateBinaryObjectAsync(BinaryObject file)
    {
        await CreateAsync(file);
    }

    public async Task DeleteBinaryObjectAsync(Guid id)
    {
        var entity = GetByCondition(o => o.Id == id).FirstOrDefault();
        await DeleteAsync(entity);
    }
}