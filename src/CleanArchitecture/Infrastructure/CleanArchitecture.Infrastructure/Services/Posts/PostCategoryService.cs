using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using Contracts.Exceptions;
using Infrastructure.Common.Models.Paging;

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

    public async Task<List<PostCategoryDto>> GetAllPostCategoriesAsync(GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryRepository.GetAllPostCategoriesAsync(input);
        return result;
    }

    public async Task<PagedResult<PostCategoryDto>> GetAllPostCategoryPagedAsync(GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryRepository.GetAllPostCategoryPagedAsync(input);
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
        await _postCategoryRepository.DeletePostCategoryAsync(ids);
    }
}