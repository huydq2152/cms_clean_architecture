using Contracts.Configurations;

namespace Infrastructure.Configurations;

public class JwtTokenSettings: IJwtTokenSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public int ExpireInHours { get; set; }
}