using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class CreateOrUpdateUserDto: FullAuditedEntityBaseDto<int?>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public DateTime? Dob { get; set; }
    public string? Avatar { get; set; }
    public bool IsActive { get; set; }
}