using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class ListDtoToEntityMapper<TDto> where TDto : IDtoObject
{
    private readonly List<TDto> _dtos;
    private readonly IMapper _mapper;

    public ListDtoToEntityMapper(List<TDto> dtos, IMapper mapper)
    {
        _dtos = dtos;
        _mapper = mapper;
    }

    public List<TEntity> Map<TEntity>() where TEntity : IEntityObject
    {
        return _dtos.Select(e => _mapper.Map<TDto, TEntity>(e)).ToList();
    }
}