using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class ListEntityToDtoMapper<TEntity> where TEntity : IEntityObject
{
    private readonly List<TEntity> _entities;
    private readonly IMapper _mapper;

    public ListEntityToDtoMapper(List<TEntity> entities, IMapper mapper)
    {
        _entities = entities;
        _mapper = mapper;
    }

    public List<TDto> Map<TDto>() where TDto : IDtoObject
    {
        return _entities.Select(e => _mapper.Map<TEntity, TDto>(e)).ToList();
    }
}