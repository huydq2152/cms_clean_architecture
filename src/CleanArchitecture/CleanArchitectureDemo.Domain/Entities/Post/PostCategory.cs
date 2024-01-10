using Contracts.Domains;

namespace CleanArchitectureDemo.Domain.Entities.Post;

public class PostCategory : FullAuditedEntityBase<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
}