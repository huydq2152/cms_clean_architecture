using CleanArchitecture.Domain.Enums;
using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Posts.Post;

public class PostDto: FullAuditedEntityBaseDto<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    
    public int CategoryId { get; set; }
    public string CategoryCode { get; set; }
    public string CategoryName { get; set; }
    
    public string Thumbnail { get; set; }
    public string Content { get; set; }
    
    public int AuthorUserId { get; set; }
    public string AuthorUserName { get; set; }
    public string AuthorFullName { get; set; }
    
    public string Source { get; set; }
    public string Tags { get; set; }
    public string SeoDescription { get; set; }
    public int ViewCount { get; set; }
    public PostStatusEnum Status { get; set; }
    public bool IsActive { get; set; }
}