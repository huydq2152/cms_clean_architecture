using System.Linq.Expressions;
using AutoMapper;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;

namespace Infrastructure.AutoMappers;

public class QueryableDtoToEntityMapper<TDto> where TDto : IDtoObject
{
    private readonly IQueryable<TDto> _dtos;
    private readonly IMapper _mapper;

    public QueryableDtoToEntityMapper(IQueryable<TDto> dtos, IMapper mapper)
    {
        _dtos = dtos;
        _mapper = mapper;
    }

    public IQueryable<TEntity> ToProjection<TEntity>() where TEntity : IEntityObject
    {
        return _mapper.ProjectTo<TEntity>(_dtos);
    }

    public IQueryable<TEntity> ToProjection<TEntity>(
        object? parameters = null,
        params Expression<Func<TEntity, object>>[] membersToExpand
    ) where TEntity : IEntityObject
    {
        return _mapper.ProjectTo<TEntity>(_dtos, parameters, membersToExpand);
    }

    public IQueryable<TEntity> ToProjection<TEntity>(
        IDictionary<string, object> parameters,
        params string[] membersToExpand
    ) where TEntity : IEntityObject
    {
        return _mapper.ProjectTo<TEntity>(_dtos, parameters, membersToExpand);
    }
}