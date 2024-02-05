namespace Infrastructure.Common.Models.Auth;

public class RoleClaims
{
    public string Type { get; set; }
    public string Value { get; set; }
    public string? DisplayName { get; set; }
    public bool Selected { get; set; }
}