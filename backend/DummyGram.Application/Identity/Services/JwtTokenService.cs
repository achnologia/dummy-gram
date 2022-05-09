using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DummyGram.Domain.Entities;
using DummyGram.Infrastructure.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DummyGram.Application.Identity.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    private readonly ApplicationDbContext _context;

    public JwtTokenService(JwtOptions jwtOptions, ApplicationDbContext context)
    {
        _jwtOptions = jwtOptions;
        _context = context;
    }
    
    public async Task<(string token, string refresh)> GenerateTokensAsync(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("idUser", user.Id)
            }),
            Expires = DateTime.UtcNow.Add(_jwtOptions.TokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var refreshToken = new RefreshToken
        {
            IdJwt = token.Id,
            IdUser = user.Id,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        var result = (tokenHandler.WriteToken(token), refreshToken.Token);

        return result;
    }
}