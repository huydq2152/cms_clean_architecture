namespace CleanArchitecture.Application.Dtos.Auth;

public class AuthenticatedResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}