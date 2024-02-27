using AutoMapper;
using CleanArchitecture.Application.Dtos.Posts;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Domain.Entities.Posts;
using Contracts.Exceptions;
using Infrastructure.Common.Models.Paging;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<PostCategoryDto>> GetAllPostCategoriesAsync()
    {
        var objQuery = await _postCategoryRepository.GetAllPostCategoriesAsync();
        var result = await objQuery.ToListAsync();
        return result;
    }

    public async Task<PagedResult<PostCategoryDto>> GetAllPostCategoryPagedAsync(PostCategoryPagingQueryInput input)
    {
        var objQuery = await _postCategoryRepository.GetAllPostCategoryPagedAsync(input);
        var result = await PagedResult<PostCategoryDto>.ToPagedListAsync(objQuery, input.PageIndex, input.PageSize);
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
            if (postCategory == null) throw new NotFoundException(nameof(PostCategory), id);
            await _postCategoryRepository.DeletePostCategoryAsync(postCategory);
        }
    }
}