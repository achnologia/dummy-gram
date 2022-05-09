namespace DummyGram.Application.Story.Services;

public interface IStoryService
{
    public Task<int> Create();

    public Task Update();

    public Task Delete();
}