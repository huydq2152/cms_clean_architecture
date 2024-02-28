using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Domain.Entities.Posts;
using Contracts.Common.Interfaces.Repositories;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Posts;

public interface IPostRepository: IRepositoryBase<Post, int>
{
    Task<PostDto> GetPostByIdAsync(int id);
    Task<List<PostDto>> GetAllPostsAsync(GetAllPostsInput input);
    Task<PagedResult<PostDto>> GetAllPostPagedAsync(GetAllPostsInput query);
    Task CreatePostAsync(CreatePostDto post);
    Task UpdatePostAsync(UpdatePostDto post);
    Task DeletePostAsync(int[] ids);
}