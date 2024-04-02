using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using Infrastructure.Common.Helpers.Paging;

namespace CleanArchitecture.WebApp.Models.Posts;

public class PostsByCategoryViewModel
{
    public PostCategoryDto Category { get; set; }
    public PagedResult<PostDto> Posts { get; set; }
}