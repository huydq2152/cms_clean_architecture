namespace Contracts.Domains.Interfaces
{
    public interface IEntityBase<T> : IEntityObject
    {
        T Id { get; set; }
    }
}