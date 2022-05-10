using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DummyGram.Domain.Entities;

[Table("AppUsers")]
public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }

    public List<AppUser> Subscriptions { get; set; } = new List<AppUser>();
    
    public List<AppUser> Subscribers { get; set; } = new List<AppUser>();
    
    public List<Post> SavedPosts { get; set; } = new List<Post>();
    
    public AppUser() { }

    public AppUser(string email)
    {
        Email = email;
        UserName = email;
        DisplayName = email;
    }

    public void Update(string displayName)
    {
        DisplayName = displayName;
    }

    public void Subscribe(AppUser newSubscriber)
    {
        Subscribers.Add(newSubscriber);
    }

    public void Unsubscribe(AppUser newSubscriber)
    {
        Subscribers.Remove(newSubscriber);
    }
    
    public bool HasSubscriber(AppUser subscriber)
    {
        return Subscribers.Contains(subscriber);
    }

    public void SavePost(Post post)
    {
        SavedPosts.Add(post);
    }
    
    public void RemoveSavedPost(Post post)
    {
        SavedPosts.Remove(post);
    }
}