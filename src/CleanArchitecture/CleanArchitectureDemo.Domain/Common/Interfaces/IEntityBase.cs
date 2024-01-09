namespace CleanArchitectureDemo.Domain.Common.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}