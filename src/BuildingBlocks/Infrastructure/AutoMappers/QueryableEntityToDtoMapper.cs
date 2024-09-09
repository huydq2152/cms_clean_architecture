using System.Linq.Expressions;
using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class QueryableEntityToDtoMapper<TEntity> where TEntity : IEntityObject
{
    private readonly IQueryable<TEntity> _entities;
    private readonly IMapper _mapper;

    public QueryableEntityToDtoMapper(IQueryable<TEntity> entities, IMapper mapper)
    {
        _entities = entities;
        _mapper = mapper;
    }

    public IQueryable<TDto> ToProjection<TDto>() where TDto : IDtoObject
    {
        return _mapper.ProjectTo<TDto>(_entities);
    }

    public IQueryable<TDto> ToProjection<TDto>(
        object? parameters = null,
        params Expression<Func<TDto, object>>[] membersToExpand
    ) where TDto : IDtoObject
    {
        return _mapper.ProjectTo<TDto>(_entities, parameters, membersToExpand);
    }

    public IQueryable<TDto> ToProjection<TDto>(
        IDictionary<string, object> parameters,
        params string[] membersToExpand
    ) where TDto : IDtoObject
    {
        return _mapper.ProjectTo<TDto>(_entities, parameters, membersToExpand);
    }

    public IEnumerable<TDto> Map<TDto>() where TDto : IDtoObject
    {
        return _entities.AsEnumerable().Select(e => _mapper.Map<TEntity, TDto>(e));
    }
}