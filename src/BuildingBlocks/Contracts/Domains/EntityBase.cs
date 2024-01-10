using Contracts.Domains.Interfaces;

namespace Contracts.Domains;

public class EntityBase<T> : IEntityBase<T>
{
    public T Id { get; set; }
}
