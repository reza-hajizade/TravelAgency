namespace TravelAgency.Infrastructure.Configurations;

public class JwtSettings
{
    public string  Key { get; set; }
    public string Issuer { get; set; }
    public int ExpireMinutes { get; set; }

}

