namespace Market.Settings;

public class JwtSettings
{
    public string SecretyKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int LifeTimeInMinutes { get; set; }
}