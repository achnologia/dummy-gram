using DummyGram.Application.Posts.Repositories;
using DummyGram.Domain.Entities;

namespace DummyGram.Application.Posts.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;

    public PostService(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreateAsync(string idUser, string imageUrl, string description)
    {
        var newPost = new Post(idUser, imageUrl, description);

        await _repository.AddAsync(newPost);

        return newPost.Id;
    }

    public async Task<bool> UpdateAsync(int id, string imageUrl, string description)
    {
        var post = await _repository.GetByIdAsync(id);

        if (post is null)
            return false;

        post.Update(imageUrl, description);

        return await _repository.UpdateAsync(post);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
    
    public async Task<bool> IsUserPostAuthor(int id, string idUser)
    {
        var post = await _repository.GetByIdNoTrackingAsync(id);

        if (post is null)
            return false;

        if (post.IdUser != idUser)
            return false;

        return true;
    }
}