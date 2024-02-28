using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts;

public class GetAllPostCategoriesInput: PagingRequestParameters
{
    public string Filter { get; set; }
}