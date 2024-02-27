using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Auth;

[Table("AppRoles")]
public class AppRole: IdentityRole<int>, IFullAuditedEntityBase
{
    [Required]
    [MaxLength(200)]
    public string DisplayName { get; set; }

    public int? CreatorUserId { get; set; }
    public DateTime CreationTime { get; set; }
    public int? LastModifiedUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public int? DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
}