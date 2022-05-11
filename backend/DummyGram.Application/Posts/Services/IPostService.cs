using DummyGram.Domain.Entities;

namespace DummyGram.Application.Posts.Services;

public interface IPostService
{
    public Task<int> CreateAsync(string idUser, string imageUrl, string description);
    public Task<bool> UpdateAsync(int id, string imageUrl, string description);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> IsUserPostAuthorAsync(int id, string idUser);
    public Task<bool> CommentAsync(int id, string idUser, string comment);
    public Task<bool> RemoveCommentAsync(int id, int idComment);
    public Task<bool> IsUserPostCommentAuthorAsync(int id, int idPostComment, string idUser);
    public Task<bool> LikeAsync(int id, string idUser);
    public Task<bool> RemoveLikeAsync(int id, string idUser);
    public Task<Post> GetPost(int id);
}