namespace DummyGram.Domain.Post.Service;

public interface IPostService
{
    public Task<int> CreateAsync();

    public Task UpdateAsync();

    public Task DeleteAsync();
}