using System.ComponentModel.DataAnnotations.Schema;

namespace DummyGram.Domain.Entities;

public class PostComment : Entity
{
    public int IdPost { get; set; }
    public string IdUser { get; set; }
    public string Comment { get; set; }
    
    [ForeignKey(nameof(IdPost))]
    public virtual Post Post { get; set; }
    
    [ForeignKey(nameof(IdUser))]
    public virtual AppUser Author { get; set; }

    public PostComment(int idPost, string idUser, string comment)
    {
        IdPost = idPost;
        IdUser = idUser;
        Comment = comment;
    }
}