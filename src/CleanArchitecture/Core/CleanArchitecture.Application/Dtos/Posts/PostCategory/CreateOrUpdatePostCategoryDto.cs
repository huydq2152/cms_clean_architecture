using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Posts.PostCategory;

public class CreateOrUpdatePostCategoryDto : FullAuditedEntityBaseDto<int?>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Slug { set; get; }
    public bool IsActive { set; get; }
    public string SeoDescription { set; get; }
    public int SortOrder { set; get; }
    
    public int? ParentId { get; set; }
}