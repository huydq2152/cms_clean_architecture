using CleanArchitecture.Application.Dtos.Posts.Post;

namespace CleanArchitecture.WebApp.Models;

public class HomeViewModel
{
    public List<PostDto> LatestPosts { get; set; }
}