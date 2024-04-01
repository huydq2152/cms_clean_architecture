using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts.PostCategory;

public class GetAllPostCategoriesInput: PagingRequestParameters
{
    public int? Id { get; set; }
    public string Keyword { get; set; }
    public int? ParentId { get; set; }
}