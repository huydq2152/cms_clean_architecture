using AutoMapper;
using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Domain.Entities.Post;

namespace CleanArchitectureDemo.WebAPI;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<PostCategory, PostCategoryDto>().ReverseMap();
    }
}