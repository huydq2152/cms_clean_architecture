using Contracts.Dtos.Interface;

namespace Contracts.Dtos;

public class EntityBaseDto<T> : IEntityBaseDto<T>
{
    public T Id { get; set; }
}