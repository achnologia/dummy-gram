namespace DummyGram.Application.Story.Services;

public interface IStoryService
{
    public Task<int> CreateAsync(string idUser, string imageUrl);

    public Task<bool> DeleteAsync(int id);
    
    public Task<bool> IsUserStoryAuthor(int id, string idUser);
}