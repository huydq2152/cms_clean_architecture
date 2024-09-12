using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Dtos.Posts.Post;

public class GetAllPostsInput: PagingRequestParameters
{
    public string Keyword { get; set; }
    public int? CategoryId { get; set; }
    public int? AuthorUserId { get; set; }
}