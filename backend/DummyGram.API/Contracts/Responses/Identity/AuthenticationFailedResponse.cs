namespace DummyGram.API.Contracts.Responses.Identity;

public class AuthenticationFailedResponse
{
    public IEnumerable<string> Errors { get; set; }
}