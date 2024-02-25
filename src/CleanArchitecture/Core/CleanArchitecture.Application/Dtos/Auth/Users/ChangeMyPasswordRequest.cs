namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class ChangeMyPasswordRequest
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}