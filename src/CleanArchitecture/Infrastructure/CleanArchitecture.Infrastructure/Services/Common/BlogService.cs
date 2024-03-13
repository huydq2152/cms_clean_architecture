using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Repositories.Posts;
using CleanArchitecture.Application.Interfaces.Services.Common;
using CleanArchitecture.Domain.Entities.Auth;
using Contracts.Exceptions;
using Infrastructure.Common.Helpers.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Collection;

namespace CleanArchitecture.Infrastructure.Services.Common;

public class BlogService : IBlogService
{
    private readonly IMapper _mapper;
    private readonly IPostCategoryRepository _postCategoryRepository;
    private readonly UserManager<AppUser> _userManager;

    public BlogService(IMapper mapper, IPostCategoryRepository postCategoryRepository, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _postCategoryRepository = postCategoryRepository;
        _userManager = userManager;
    }

    #region User

    public async Task<UserDto> GetBlogUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new NotFoundException(nameof(AppUser), id);
        }

        var result = _mapper.Map<UserDto>(user);
        return result;
    }

    public async Task<List<UserDto>> GetAllBlogUsersAsync(GetAllUsersInput input)
    {
        var users = await _userManager.Users
            .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Keyword),
                x => x.UserName.Contains(input.Keyword)
                     || x.Email.Contains(input.Keyword)).ToListAsync();

        var result = _mapper.Map<List<UserDto>>(users);
        return result;
    }

    public async Task<PagedResult<UserDto>> GetAllBlogUsersPagedAsync(GetAllUsersInput input)
    {
        var query = _userManager.Users
            .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Keyword),
                x => x.UserName.Contains(input.Keyword)
                     || x.Email.Contains(input.Keyword));

        var objQuery = _mapper.ProjectTo<UserDto>(query);

        var result = await PagedResult<UserDto>.ToPagedListAsync(objQuery, input.PageIndex, input.PageSize);
        return result;
    }

    #endregion

    #region PostCategory

    public async Task<PostCategoryDto> GetBlogPostCategoryByIdAsync(int id)
    {
        var result = await _postCategoryRepository.GetPostCategoryByIdAsync(id);
        return result;
    }

    public async Task<List<PostCategoryDto>> GetBlogAllPostCategoriesAsync(GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryRepository.GetAllPostCategoriesAsync(input);
        return result;
    }

    public async Task<PagedResult<PostCategoryDto>> GetBlogAllPostCategoryPagedAsync(GetAllPostCategoriesInput input)
    {
        var result = await _postCategoryRepository.GetAllPostCategoriesPagedAsync(input);
        return result;
    }

    #endregion
}