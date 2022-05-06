using DummyGram.Domain.Post.Repository;
using DummyGram.Domain.Post.Service;
using DummyGram.Domain.Story.Repository;
using DummyGram.Domain.Story.Service;
using Microsoft.Extensions.DependencyInjection;

namespace DummyGram.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        
        services.AddScoped<IStoryRepository, StoryRepository>();
        services.AddScoped<IStoryService, StoryService>();
        
        return services;
    }
}