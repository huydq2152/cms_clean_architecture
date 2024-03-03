using Contracts.Domains.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Auth;

public class AppUser: IdentityUser<int>, IFullAuditedEntityBase
{
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public bool IsActive { get; set; }
   public string RefreshToken { get; set; }
   public DateTime? RefreshTokenExpiryTime { get; set; }
   public DateTime? Dob { get; set; }
   public string Avatar { get; set; }
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