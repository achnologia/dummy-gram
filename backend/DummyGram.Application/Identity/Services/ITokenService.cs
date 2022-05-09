using Microsoft.AspNetCore.Identity;

namespace DummyGram.Application.Identity.Services;

public interface ITokenService
{
    Task<(string token, string refresh)> GenerateTokensAsync(IdentityUser user);
}