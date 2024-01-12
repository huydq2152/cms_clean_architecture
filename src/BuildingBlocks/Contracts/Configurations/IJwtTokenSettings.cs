namespace Contracts.Configurations;

public interface IJwtTokenSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public int ExpireInHours { get; set; }
}