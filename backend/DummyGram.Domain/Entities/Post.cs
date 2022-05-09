﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DummyGram.Domain.Entities;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string IdUser { get; set; }
    
    public string ImageUrl { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DatePosted { get; set; }
    
    [ForeignKey(nameof(IdUser))]
    public virtual IdentityUser Author { get; set; }

    public Post() { }
    
    public Post(string idAuthor, string imageUrl, string description)
    {
        IdUser = idAuthor;
        ImageUrl = imageUrl;
        Description = description;
        
        DatePosted = DateTime.UtcNow;
    }
}