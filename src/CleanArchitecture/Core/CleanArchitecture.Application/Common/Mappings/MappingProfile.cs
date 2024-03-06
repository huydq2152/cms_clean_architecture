using System.Reflection;
using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth.Roles;
using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Domain.Entities.Posts;
using Shared.Extensions.Mapping;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            
            #region Auth

            CreateMap<AppRole, RoleDto>();
            CreateMap<AppRole, CreateRoleDto>();
            CreateMap<AppRole, UpdateRoleDto>();
            
            CreateMap<AppUser, UserDto>();
            CreateMap<CreateUserDto, AppUser>();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();

            #endregion
            
            #region Post

            CreateMap<PostCategoryDto, PostCategory>();
            CreateMap<CreatePostCategoryDto, PostCategory>();
            CreateMap<UpdatePostCategoryDto, PostCategory>();
            
            CreateMap<PostDto, Post>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();

            #endregion
            
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
