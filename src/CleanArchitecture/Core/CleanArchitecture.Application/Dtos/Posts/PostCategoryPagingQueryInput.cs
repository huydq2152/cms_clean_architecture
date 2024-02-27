using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts;

public class PostCategoryPagingQueryInput: PagingRequestParameters
{
    public string Filter { get; set; }
}