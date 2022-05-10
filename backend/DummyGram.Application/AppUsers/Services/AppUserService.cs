using DummyGram.Application.AppUsers.Repositories;
using DummyGram.Application.Posts.Repositories;

namespace DummyGram.Application.AppUsers.Services;

public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _repository;
    private readonly IPostRepository _postRepository;

    public AppUserService(IAppUserRepository repository, IPostRepository postRepository)
    {
        _repository = repository;
        _postRepository = postRepository;
    }

    public async Task<bool> UpdateAsync(string id, string displayName)
    {
        var appUser = await _repository.GetByIdAsync(id);

        if (appUser is null)
            return false;

        appUser.Update(displayName);

        return await _repository.UpdateAsync(appUser);
    }

    public async Task<bool> SubscribeAsync(string idUserSubscriber, string idUserSubscribeTo)
    {
        var appUserSub = await _repository.GetByIdAsync(idUserSubscriber);
        var appUserSubTo = await _repository.GetByIdAsync(idUserSubscribeTo);

        if (appUserSub is null || appUserSubTo is null)
            return false;

        if (appUserSubTo.HasSubscriber(appUserSub))
            return false;
        
        appUserSubTo.Subscribe(appUserSub);
        
        return await _repository.UpdateAsync(appUserSubTo);
    }

    public async Task<bool> UnsubscribeAsync(string idUserSubscriber, string idUserSubscribedTo)
    {
        var appUserSub = await _repository.GetByIdAsync(idUserSubscriber);
        var appUserSubTo = await _repository.GetByIdAsync(idUserSubscribedTo);

        if (appUserSub is null || appUserSubTo is null)
            return false;

        appUserSubTo.Unsubscribe(appUserSub);
        
        return await _repository.UpdateAsync(appUserSubTo);
    }

    public async Task<bool> SavePostAsync(string id, int idPost)
    {
        var appUser = await _repository.GetByIdAsync(id);
        var post = await _postRepository.GetByIdAsync(idPost);
        
        if (appUser is null || post is null)
            return false;

        appUser.SavePost(post);

        return await _repository.UpdateAsync(appUser);
    }

    public async Task<bool> RemoveSavedPostAsync(string id, int idPost)
    {
        var appUser = await _repository.GetByIdAsync(id);
        var post = await _postRepository.GetByIdAsync(idPost);

        if (appUser is null || post is null)
            return false;

        appUser.RemoveSavedPost(post);

        return await _repository.UpdateAsync(appUser);
    }
}