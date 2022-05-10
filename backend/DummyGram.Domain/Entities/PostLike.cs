using System.ComponentModel.DataAnnotations.Schema;

namespace DummyGram.Domain.Entities;

public class PostLike
{
    public int IdPost { get; set; }
    public string IdUser { get; set; }
    
    [ForeignKey(nameof(IdPost))]
    public virtual Post Post { get; set; }
    
    [ForeignKey(nameof(IdUser))]
    public virtual AppUser LikedBy { get; set; }

    public PostLike(int idPost, string idUser)
    {
        IdPost = idPost;
        IdUser = idUser;
    }
}