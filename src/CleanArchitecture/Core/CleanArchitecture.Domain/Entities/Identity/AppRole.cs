using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Identity;

[Table("AppRoles")]
public class AppRole: IdentityRole<int>
{
    [Required]
    [MaxLength(200)]
    public string DisplayName { get; set; }
}