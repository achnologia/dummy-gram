namespace DummyGram.Application.Identity;

public class JwtOptions
{
    public string Secret { get; set; }
    public TimeSpan TokenLifetime { get; set; }
}