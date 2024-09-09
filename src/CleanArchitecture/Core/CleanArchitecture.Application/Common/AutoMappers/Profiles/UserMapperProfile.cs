using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Domain.Entities.Auth;

namespace CleanArchitecture.Application.Common.AutoMappers.Profiles;

public class UserMapperProfile: Profile
{
    public UserMapperProfile()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<CreateUserDto, AppUser>();
        CreateMap<AppUser, UpdateUserDto>().ReverseMap();
    }
}