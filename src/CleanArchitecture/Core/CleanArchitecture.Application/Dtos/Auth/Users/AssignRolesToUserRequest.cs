namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class AssignRolesToUserRequest
{
    public int CurrentUserId { get; set; }
    public string[] Roles { get; set; }
}