using Contracts.Domains;

namespace CleanArchitecture.Domain.Entities.Apps;

public class BinaryObject: EntityBase<Guid>
{
    public virtual string Description { get; set; }
    public virtual byte[] Bytes { get; set; }

    public BinaryObject()
    {
        Id = Guid.NewGuid();
    }

    public BinaryObject(byte[] bytes, string description = null)
        : this()
    {
        Bytes = bytes;
        Description = description;
    }
}