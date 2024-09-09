using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Domain.Entities.Posts;

namespace CleanArchitecture.Application.Common.AutoMappers.Profiles;

public class PostCategoryMapperProfile: Profile
{
    public PostCategoryMapperProfile()
    {
        CreateMap<PostCategoryDto, PostCategory>();
        CreateMap<CreatePostCategoryDto, PostCategory>();
        CreateMap<UpdatePostCategoryDto, PostCategory>();
    }
}