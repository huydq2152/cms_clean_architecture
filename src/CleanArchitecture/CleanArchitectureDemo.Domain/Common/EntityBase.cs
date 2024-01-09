using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitectureDemo.Domain.Common.Interfaces;

namespace CleanArchitectureDemo.Domain.Common;

public class EntityBase : IEntityBase
{
    public int Id { get; set; }
}
