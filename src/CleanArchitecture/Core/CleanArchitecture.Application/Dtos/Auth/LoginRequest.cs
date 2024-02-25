namespace CleanArchitecture.Application.Dtos.Auth;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}