using Contracts.Domains;

namespace CleanArchitecture.Domain.Entities.Posts;

public class PostCategory : FullAuditedEntityBase<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Slug { set; get; }
    public bool IsActive { set; get; }
    public string SeoDescription { set; get; }
    public int SortOrder { set; get; }
    public int? ParentId { get; set; }
    public PostCategory Parent { get; set; }
    public ICollection<PostCategory> Children { get; set; }
}