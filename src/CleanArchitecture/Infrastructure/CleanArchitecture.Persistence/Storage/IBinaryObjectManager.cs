using CleanArchitecture.Domain.Entities.Apps;

namespace CleanArchitecture.Persistence.Storage;

public interface IBinaryObjectManager
{
    Task<BinaryObject> GetOrNullAsync(Guid id);
        
    Task CreateBinaryObjectAsync(BinaryObject file);
        
    Task DeleteBinaryObjectAsync(Guid id);
}