using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DummyGram.Domain.Entities;

public class Story : Entity
{
    public string IdUser { get; set; }
    
    public string ImageUrl { get; set; }

    public DateTime DatePosted { get; set; }
    
    public DateTime DateOfExpiration { get; set; }
    
    [ForeignKey(nameof(IdUser))]
    public virtual IdentityUser Author { get; set; }

    public Story() { }
    
    public Story(string idAuthor, string imageUrl)
    {
        IdUser = idAuthor;
        ImageUrl = imageUrl;

        DatePosted = DateTime.UtcNow;
        DateOfExpiration = DatePosted.AddHours(24);
    }
}