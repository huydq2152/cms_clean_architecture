namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class ChangeEmailRequest
{
    public int CurrentUserId { get; set; }
    public string Email { get; set; }
}