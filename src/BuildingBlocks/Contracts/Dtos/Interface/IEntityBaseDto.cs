namespace Contracts.Dtos.Interface
{
    public interface IEntityBaseDto<T>
    {
        T Id { get; set; }
    }
}