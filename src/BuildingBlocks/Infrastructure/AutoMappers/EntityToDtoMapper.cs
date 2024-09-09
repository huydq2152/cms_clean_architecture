using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class EntityToDtoMapper<TEntity> where TEntity : IEntityObject
{
    private readonly TEntity _entity;
    private readonly IMapper _mapper;

    public EntityToDtoMapper(TEntity entity, IMapper mapper)
    {
        _entity = entity;
        _mapper = mapper;
    }

    public TDto Map<TDto>() where TDto : IDtoObject
    {
        return _mapper.Map<TEntity, TDto>(_entity);
    }
}