using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Domain.Entities.Auth;

namespace CleanArchitecture.Application.Common.AutoMappers.Profiles;

public class RoleMapperProfile : Profile
{
    public RoleMapperProfile()
    {
        CreateMap<AppRole, RoleDto>();
        CreateMap<AppRole, CreateRoleDto>();
        CreateMap<AppRole, UpdateRoleDto>();
    }
}