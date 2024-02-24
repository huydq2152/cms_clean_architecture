namespace Infrastructure.Common.Models.Auth
{
    public class TokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}