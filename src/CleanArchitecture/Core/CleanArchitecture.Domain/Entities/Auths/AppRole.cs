using Contracts.Domains.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Auths;

public class AppRole: IdentityRole<int>, IFullAuditedEntityBase
{
    public string DisplayName { get; set; }

    public int? CreatorUserId { get; set; }
    public DateTime CreationTime { get; set; }
    public int? LastModifiedUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public int? DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
}