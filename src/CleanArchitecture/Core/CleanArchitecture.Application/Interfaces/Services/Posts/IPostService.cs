﻿using CleanArchitecture.Application.Dtos.Posts.Post;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Posts;

public interface IPostService
{
    Task<PostDto> GetPostByIdAsync(int id);
    Task<List<PostDto>> GetAllPostsAsync(GetAllPostsInput input);
    Task<PagedResult<PostDto>> GetAllPostPagedAsync(GetAllPostsInput input);
    Task CreatePostAsync(CreatePostDto post);
    Task UpdatePostAsync(UpdatePostDto post);
    Task DeletePostsAsync(int[] ids);
}