using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class EnumerableDtoToEntityMapper<TDto> where TDto : IDtoObject
{
    private readonly IEnumerable<TDto> _dtos;
    private readonly IMapper _mapper;

    public EnumerableDtoToEntityMapper(IEnumerable<TDto> dtos, IMapper mapper)
    {
        _dtos = dtos;
        _mapper = mapper;
    }

    public IEnumerable<TEntity> Map<TEntity>() where TEntity : IEntityObject
    {
        return _dtos.Select(e => _mapper.Map<TDto, TEntity>(e));
    }
}