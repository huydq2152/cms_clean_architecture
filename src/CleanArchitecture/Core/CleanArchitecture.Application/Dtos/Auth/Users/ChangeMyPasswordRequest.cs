namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class ChangeMyPasswordRequest
{
    public int CurrentUserId { get; set; }
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}