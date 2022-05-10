using DummyGram.Application.Stories.Repositories;
using DummyGram.Domain.Entities;

namespace DummyGram.Application.Stories.Services;

public class StoryService : IStoryService
{
    private readonly IStoryRepository _repository;

    public StoryService(IStoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreateAsync(string idUser, string imageUrl)
    {
        var newStory = new Story(idUser, imageUrl);
        
        await _repository.AddAsync(newStory);

        return newStory.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
    
    public async Task<bool> IsUserStoryAuthor(int id, string idUser)
    {
        var story = await _repository.GetByIdNoTrackingAsync(id);

        if (story is null)
            return false;

        if (story.IdUser != idUser)
            return false;

        return true;
    }
}