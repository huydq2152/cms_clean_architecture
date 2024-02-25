namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class SetPasswordRequest
{
    public int CurrentUserId { get; set; }
    public string NewPassword { get; set; }
}