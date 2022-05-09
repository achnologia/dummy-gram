namespace DummyGram.Application.Post.Services;

public interface IPostService
{
    public Task<int> CreateAsync();

    public Task UpdateAsync();

    public Task DeleteAsync();
}