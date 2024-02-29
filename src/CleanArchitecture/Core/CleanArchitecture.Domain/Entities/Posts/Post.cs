using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Domain.Enums;
using Contracts.Domains;

namespace CleanArchitecture.Domain.Entities.Posts;

public class Post: FullAuditedEntityBase<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    
    public int CategoryId { get; set; }
    public PostCategory Category { get; set; }
    
    public string Thumbnail { get; set; }
    public string Content { get; set; }
    
    public int AuthorUserId { get; set; }
    public AppUser Author { get; set; }
    public string Source { get; set; }
    public string Tags { get; set; }
    public string SeoDescription { get; set; }
    public int ViewCount { get; set; }
    public PostStatusEnum Status { get; set; }
    public bool IsActive { get; set; }
}