using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts.PostCategory;

public class GetAllPostCategoriesInput: PagingRequestParameters
{
    public string Filter { get; set; }
}