namespace DummyGram.Application.Post.Services;

public interface IPostService
{
    public Task<int> CreateAsync(string idUser, string imageUrl, string description);

    public Task<bool> UpdateAsync(int id, string imageUrl, string description);

    public Task<bool> DeleteAsync(int id);

    public Task<bool> IsUserPostAuthor(int id, string idUser);
}