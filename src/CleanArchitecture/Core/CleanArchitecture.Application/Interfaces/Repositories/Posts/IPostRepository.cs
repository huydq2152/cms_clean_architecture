using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Excels.Exporting.Dtos;
using CleanArchitecture.Domain.Entities.Posts;
using Contracts.Common.Interfaces.Repositories;
using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Posts;

public interface IPostRepository: IRepositoryBase<Post, int>
{
    Task<PostDto> GetPostByIdAsync(int id);
    Task<List<PostDto>> GetAllPostsAsync(GetAllPostsInput input);
    Task<PagedResult<PostDto>> GetAllPostPagedAsync(GetAllPostsInput input);
    Task CreatePostAsync(CreatePostDto post);
    Task UpdatePostAsync(UpdatePostDto post);
    Task DeletePostAsync(int[] ids);
    Task<List<PostDto>> GetPostsByCategoryIdAsync(int id);
    Task<List<PostDto>> GetLatestPublishedPostsAsync(int numOfPosts);
    Task<PagedResult<PostDto>> GetPostPagedByCategoryIdAsync(GetAllPostsInput input);
    Task<PostDto> GetPostBySlugAsync(string slug);
    Task<List<ExportPostDto>> GetAllPostsForExportAsync(GetExportPostsInput input);
}