using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitectureDemo.Domain.Common.Interfaces;

namespace CleanArchitectureDemo.Domain.Common;

public class EntityBase<T> : IEntityBase<T>
{
    public T Id { get; set; }
}
