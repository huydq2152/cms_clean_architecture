using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using Infrastructure.Common.Helpers.Paging;
using ApplicationException = Contracts.Exceptions.ApplicationException;

namespace CleanArchitecture.Infrastructure.Services.Posts;

public class PostCategoryService : IPostCategoryService
{
    private readonly IPostCategoryRepository _postCategoryRepository;
    private readonly IPostRepository _postRepository;

    public PostCategoryService(IPostCategoryRepository postCategoryRepository, IPostRepository postRepository)
    {
        _postCategoryRepository = postCategoryRepository;
        _postRepository = postRepository;
    }

    public async Task<PostCategoryDto> GetPostCategoryByIdAsync(int id)
    {
        var result = await _postCategoryRepository.GetPostCategoryByIdAsync(id);
        return result;
    }

    public async Task<List<PostCategoryDto>> GetAllPostCategoriesAsync(GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryRepository.GetAllPostCategoriesAsync(input);
        return result;
    }

    public async Task<PagedResult<PostCategoryDto>> GetAllPostCategoryPagedAsync(GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryRepository.GetAllPostCategoriesPagedAsync(input);
        return result;
    }

    public async Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory)
    {
        await _postCategoryRepository.CreatePostCategoryAsync(postCategory);
    }

    public async Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory)
    {
        await _postCategoryRepository.UpdatePostCategoryAsync(postCategory);
    }

    public async Task DeletePostCategoriesAsync(int[] ids)
    {
        foreach (var id in ids)
        {
            var posts = await _postRepository.GetPostsByCategoryIdAsync(id);
            if (posts.Any())
            {
                throw new ApplicationException("Exist posts still not delete in this category");
            }
            await _postCategoryRepository.DeletePostCategoryAsync(id);
        }
    }
}