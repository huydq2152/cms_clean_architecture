namespace Infrastructure.Common.Models.Auth;

public class AuthenticatedResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}