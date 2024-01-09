using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureDemo.Domain.Entities.Identity;

[Table("AppUsers")]
public class AppUser: IdentityUser<int>
{
   [Required]
   [MaxLength(100)]
   public string FirstName { get; set; }
   
   [Required]
   [MaxLength(100)]
   public string LastName { get; set; }
   
   public bool IsActive { get; set; }
   public string RefreshToken { get; set; }
   public string RefreshTokenExpireTime { get; set; }
}