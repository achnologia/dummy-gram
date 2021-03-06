using System.Text;
using DummyGram.Application.AppUsers.Repositories;
using DummyGram.Application.AppUsers.Services;
using DummyGram.Application.Identity;
using DummyGram.Application.Identity.Services;
using DummyGram.Application.Posts.Repositories;
using DummyGram.Application.Posts.Services;
using DummyGram.Application.Stories.Repositories;
using DummyGram.Application.Stories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DummyGram.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        configuration.Bind(nameof(jwtOptions), jwtOptions);
        services.AddSingleton(jwtOptions);

        services.AddScoped<ITokenService, JwtTokenService>();

        services.AddScoped<IIdentityService, IdentityService>();
        
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        
        services.AddScoped<IStoryRepository, StoryRepository>();
        services.AddScoped<IStoryService, StoryService>();
        
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IAppUserService, AppUserService>();
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret))
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret))
                };
            });
        
        return services;
    }
}