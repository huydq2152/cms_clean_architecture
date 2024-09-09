using Euroland.FlipIT.SData.API.Dto;

namespace Contracts.Dtos.Interface
{
    public interface IEntityBaseDto<T> : IDtoObject
    {
        T Id { get; set; }
    }
}