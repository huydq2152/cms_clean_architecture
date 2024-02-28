using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts.Post;

public class GetAllPostsInput: PagingRequestParameters
{
    public string Filter { get; set; }
}