using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DummyGram.Domain.Entities;

[Table("AppUsers")]
public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    
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
}