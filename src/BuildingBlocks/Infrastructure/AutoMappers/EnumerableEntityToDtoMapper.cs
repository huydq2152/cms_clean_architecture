using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class EnumerableEntityToDtoMapper<TEntity> where TEntity : IEntityObject
{
    private readonly IEnumerable<TEntity> _entities;
    private readonly IMapper _mapper;

    public EnumerableEntityToDtoMapper(IEnumerable<TEntity> entities, IMapper mapper)
    {
        _entities = entities;
        _mapper = mapper;
    }

    public IEnumerable<TDto> Map<TDto>() where TDto : IDtoObject
    {
        return _entities.Select(e => _mapper.Map<TEntity, TDto>(e));
    }
}