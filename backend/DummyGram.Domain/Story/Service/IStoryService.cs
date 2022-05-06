namespace DummyGram.Domain.Story.Service;

public interface IStoryService
{
    public Task<int> Create();

    public Task Update();

    public Task Delete();
}