using DummyGram.Domain.Post.Repository;
using DummyGram.Domain.Post.Service;
using Microsoft.Extensions.DependencyInjection;

namespace DummyGram.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        
        return services;
    }
}