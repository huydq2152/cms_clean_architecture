using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Domain.Entities.Post;

namespace CleanArchitecture.Infrastructure.Services.Posts;

public class PostCategoryService : IPostCategoryService
{
    private readonly IPostCategoryRepository _postCategoryRepository;
    private readonly IMapper _mapper;

    public PostCategoryService(IPostCategoryRepository postCategoryRepository, IMapper mapper)
    {
        _postCategoryRepository = postCategoryRepository;
        _mapper = mapper;
    }

    public async Task<PostCategoryDto> GetPostCategoryByIdAsync(int id)
    {
        var postCategory = await _postCategoryRepository.GetPostCategoryByIdAsync(id);
        var result = _mapper.Map<PostCategoryDto>(postCategory);
        return result;
    }

    public async Task<IEnumerable<PostCategoryDto>> GetAllPostCategoriesAsync()
    {
        var postCategories = await _postCategoryRepository.GetAllPostCategoriesAsync();
        var result = _mapper.Map<IEnumerable<PostCategoryDto>>(postCategories);
        return result;
    }

    public async Task<IEnumerable<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput input)
    {
        var postCategories = await _postCategoryRepository.GetAllPostCategoryPagedAsync(input);
        var result = _mapper.Map<IEnumerable<PostCategoryDto>>(postCategories);
        return result;
    }

    public async Task CreatePostCategoryAsync(CreatePostCategoryDto postCategory)
    {
        var postCategoryEntity = _mapper.Map<PostCategory>(postCategory);
        await _postCategoryRepository.CreatePostCategoryAsync(postCategoryEntity);
    }

    public async Task UpdatePostCategoryAsync(UpdatePostCategoryDto postCategory)
    {
        var postCategoryEntity = _mapper.Map<PostCategory>(postCategory);
        await _postCategoryRepository.UpdatePostCategoryAsync(postCategoryEntity);
    }

    public async Task DeletePostCategoriesAsync(int[] ids)
    {
        foreach (var id in ids)
        {
            var postCategory = await _postCategoryRepository.GetPostCategoryByIdAsync(id);
            if (postCategory == null) throw new Exception("Post category not found");
            await _postCategoryRepository.DeletePostCategoryAsync(postCategory);
        }
    }
}