using CleanArchitecture.Application.Dtos.Posts.Post;

namespace CleanArchitecture.WebApp.Models.Common;

public class HomeViewModel
{
    public List<PostDto> LatestPosts { get; set; }
}