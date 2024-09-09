using AutoMapper;
using CleanArchitecture.Application.Common.AutoMappers.Profiles;
using Contracts.Domains.Interfaces;
using Euroland.FlipIT.SData.API.Dto;
using Infrastructure.AutoMappers;

namespace CleanArchitecture.Application.Common.AutoMappers;

public static class AutoMapperConfigurations
{
    private static readonly MapperConfiguration _configuration = new(
        cfg =>
        {
            cfg.AddProfile<RoleMapperProfile>();
            cfg.AddProfile<UserMapperProfile>();
            cfg.AddProfile<PostCategoryMapperProfile>();
            cfg.AddProfile<PostMapperProfile>();
        }
    );

    private static readonly IMapper _mapper = new Mapper(_configuration);

    public static IMapper Mapper => _mapper;
    
    public static EntityToDtoMapper<TEntity> EntityToDtoMapper<TEntity>(this TEntity entity)
        where TEntity : IEntityObject
    {
        return new EntityToDtoMapper<TEntity>(entity, _mapper);
    }
    
    public static DtoToEntityMapper<TDto> DtoToEntityMapper<TDto>(this TDto dto)
        where TDto : IDtoObject
    {
        return new DtoToEntityMapper<TDto>(dto, _mapper);
    }

    public static QueryableEntityToDtoMapper<TEntity> EntityToDtoMapper<TEntity>(this IQueryable<TEntity> entities)
        where TEntity : IEntityObject
    {
        return new QueryableEntityToDtoMapper<TEntity>(entities, _mapper);
    }
    
    public static QueryableDtoToEntityMapper<TDto> DtoToEntityMapper<TDto>(this IQueryable<TDto> dtos)
        where TDto : IDtoObject
    {
        return new QueryableDtoToEntityMapper<TDto>(dtos, _mapper);
    }
    
    public static ListEntityToDtoMapper<TEntity> EntityToDtoMapper<TEntity>(this List<TEntity> entities)
        where TEntity : IEntityObject
    {
        return new ListEntityToDtoMapper<TEntity>(entities, _mapper);
    }
    
    public static ListDtoToEntityMapper<TDto> DtoToEntityMapper<TDto>(this List<TDto> dtos)
        where TDto : IDtoObject
    {
        return new ListDtoToEntityMapper<TDto>(dtos, _mapper);
    }

    public static EnumerableEntityToDtoMapper<TEntity> EntityToDtoMapper<TEntity>(this IEnumerable<TEntity> entities)
        where TEntity : IEntityObject
    {
        return new EnumerableEntityToDtoMapper<TEntity>(entities, _mapper);
    }
    
    public static EnumerableDtoToEntityMapper<TDto> DtoToEntityMapper<TDto>(this IEnumerable<TDto> dtos)
        where TDto : IDtoObject
    {
        return new EnumerableDtoToEntityMapper<TDto>(dtos, _mapper);
    }
}