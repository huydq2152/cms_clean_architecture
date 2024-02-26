using Shared.SeedWork;
using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts;

public class PostCategoryPagingQueryInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}