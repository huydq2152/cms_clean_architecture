using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using Infrastructure.Common.Helpers.Paging;

namespace CleanArchitecture.Infrastructure.Services.Posts;

public class PostService: IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
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

    public async Task<List<PostDto>> GetLatestPostsAsync(int numOfPosts)
    {
        var result = await _postRepository.GetLatestPostsAsync(numOfPosts);
        return result;
    }
}