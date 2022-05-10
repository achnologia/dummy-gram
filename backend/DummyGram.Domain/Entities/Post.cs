using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DummyGram.Domain.Entities;

public class Post : Entity
{
    public string IdUser { get; set; }
    
    public string ImageUrl { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DatePosted { get; set; }
    
    [ForeignKey(nameof(IdUser))]
    public virtual IdentityUser Author { get; set; }

    public List<PostComment> Comments { get; set; } = new List<PostComment>();
    
    public List<PostLike> Likes { get; set; } = new List<PostLike>();

    public List<AppUser> SavedBy { get; set; } = new List<AppUser>();
    
    public Post() { }
    
    public Post(string idAuthor, string imageUrl, string description)
    {
        IdUser = idAuthor;
        ImageUrl = imageUrl;
        Description = description;
        
        DatePosted = DateTime.UtcNow;
    }

    public void Update(string imageUrl, string description)
    {
        ImageUrl = imageUrl;
        Description = description;
    }

    public void AddComment(PostComment comment)
    {
        Comments.Add(comment);
    }

    public void RemoveComment(int idPostComment)
    {
        var postCommentToRemove = GetPostCommentById(idPostComment);

        Comments.Remove(postCommentToRemove);
    }

    public PostComment GetPostCommentById(int idPostComment)
    {
        return Comments.SingleOrDefault(x => x.Id == idPostComment);
    }
    
    public void Like(PostLike like)
    {
        Likes.Add(like);
    }

    public void RemoveLike(string idUser)
    {
        var likeToRemove = Likes.SingleOrDefault(x => x.IdUser == idUser);

        Likes.Remove(likeToRemove);
    }
}