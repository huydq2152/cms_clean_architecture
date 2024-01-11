using AutoMapper;
using CleanArchitectureDemo.Application.Dtos.Posts;
using CleanArchitectureDemo.Application.Interfaces.Repositories.Posts;
using CleanArchitectureDemo.Application.Interfaces.Services.Posts;

namespace CleanArchitectureDemo.Infrastructure.Services.Posts;

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

    public async Task<IEnumerable<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput query)
    {
        var postCategories = await _postCategoryRepository.GetAllPostCategoryPagedAsync(query);
        var result = _mapper.Map<IEnumerable<PostCategoryDto>>(postCategories);
        return result;
    }
}