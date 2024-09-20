using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Excels.Exporting.Dtos;
using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Posts;

public interface IPostService
{
    Task<PostDto> GetPostByIdAsync(int id);
    Task<List<PostDto>> GetAllPostsAsync(GetAllPostsInput input);
    Task<PagedResult<PostDto>> GetAllPostPagedAsync(GetAllPostsInput input);
    Task CreatePostAsync(CreatePostDto post);
    Task UpdatePostAsync(UpdatePostDto post);
    Task DeletePostsAsync(int[] ids);
    Task<List<PostDto>> GetLatestPublishedPostsAsync(int numOfPosts);
    Task<PagedResult<PostDto>> GetPostPagedByCategorySlugAsync(GetAllPostsInput input, string categorySlug);
    Task<PostDto> GetPostBySlugAsync(string slug);
    Task<List<ExportPostDto>> GetAllPostsForExportAsync(GetExportPostsInput input);
}