using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Dtos.Posts.PostCategory;

namespace CleanArchitecture.WebApp.Models.Posts;

public class PostDetailViewModel
{
    public PostDto Post { get; set; }
    public PostCategoryDto Category { get; set; }
}