using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class DtoToEntityMapper<TDto> where TDto : IDtoObject
{
    private readonly TDto _dto;
    private readonly IMapper _mapper;

    public DtoToEntityMapper(TDto dto, IMapper mapper)
    {
        _dto = dto;
        _mapper = mapper;
    }

    public TEntity Map<TEntity>() where TEntity : IEntityObject
    {
        return _mapper.Map<TDto, TEntity>(_dto);
    }
}