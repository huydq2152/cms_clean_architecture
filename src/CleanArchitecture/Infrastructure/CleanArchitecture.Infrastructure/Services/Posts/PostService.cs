using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using Infrastructure.Common.Helpers.Paging;

namespace CleanArchitecture.Infrastructure.Services.Posts;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IPostCategoryRepository _postCategoryRepository;

    public PostService(IPostRepository postRepository, IPostCategoryRepository postCategoryRepository)
    {
        _postRepository = postRepository;
        _postCategoryRepository = postCategoryRepository;
    }

    public async Task<PostDto> GetPostByIdAsync(int id)
    {
        var result = await _postRepository.GetPostByIdAsync(id);
        return result;
    }

    public async Task<List<PostDto>> GetAllPostsAsync(GetAllPostsInput input)
    {
        var result = await _postRepository.GetAllPostsAsync(input);
        return result;
    }

    public async Task<PagedResult<PostDto>> GetAllPostPagedAsync(GetAllPostsInput input)
    {
        var result = await _postRepository.GetAllPostPagedAsync(input);
        return result;
    }

    public async Task CreatePostAsync(CreatePostDto post)
    {
        await _postRepository.CreatePostAsync(post);
    }

    public async Task UpdatePostAsync(UpdatePostDto post)
    {
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task DeletePostsAsync(int[] ids)
    {
        await _postRepository.DeletePostAsync(ids);
    }

    public async Task<List<PostDto>> GetLatestPublishedPostsAsync(int numOfPosts)
    {
        var result = await _postRepository.GetLatestPublishedPostsAsync(numOfPosts);
        return result;
    }

    public async Task<PagedResult<PostDto>> GetPostPagedByCategorySlugAsync(GetAllPostsInput input, string categorySlug)
    {
        var postCategory = await _postCategoryRepository.GetPostCategoryBySlug(categorySlug);
        if (postCategory != null)
        {
            input.CategoryId = postCategory.Id;
        }

        var result = await _postRepository.GetPostPagedByCategoryIdAsync(input);
        return result;
    }

    public async Task<PostDto> GetPostBySlugAsync(string slug)
    {
        var result = await _postRepository.GetPostBySlugAsync(slug);
        return result;
    }
}