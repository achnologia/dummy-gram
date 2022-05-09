namespace DummyGram.API.Contracts.Requests.Identity;

public class UserLoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}