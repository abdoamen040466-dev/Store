namespace Store.Shared;

public class JwtOptions
{
    public string Issure { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public string DurationInDays { get; set; }
}
