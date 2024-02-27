using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Identity;

[Table("AppUsers")]
public class AppUser: IdentityUser<int>, IFullAuditedEntityBase
{
   [Required]
   [MaxLength(100)]
   public string FirstName { get; set; }
   
   [Required]
   [MaxLength(100)]
   public string LastName { get; set; }
   
   public bool IsActive { get; set; }
   public string? RefreshToken { get; set; }
   public DateTime? RefreshTokenExpiryTime { get; set; }
   public DateTime? Dob { get; set; }
   public int? CreatorUserId { get; set; }
   public DateTime CreationTime { get; set; }
   
   public string GetFullName()
   {
      return FirstName + " " + LastName;
   }

   public int? LastModifiedUserId { get; set; }
   public DateTime? LastModificationTime { get; set; }
   public int? DeleterUserId { get; set; }
   public DateTime? DeletionTime { get; set; }
   public bool IsDeleted { get; set; }
}