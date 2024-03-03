using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class UserDto: FullAuditedEntityBaseDto<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? Dob { get; set; }
    public string Avatar { get; set; }
    public bool IsActive { get; set; }
    
    public IList<string> Roles { get; set; }
    public string FullName => FirstName + " " + LastName;
}