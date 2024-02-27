using Contracts.Domains;

namespace CleanArchitecture.Domain.Entities.Posts;

public class PostCategory : FullAuditedEntityBase<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
}