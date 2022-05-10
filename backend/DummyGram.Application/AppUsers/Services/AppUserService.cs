using DummyGram.Application.AppUsers.Repositories;

namespace DummyGram.Application.AppUsers.Services;

public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _repository;

    public AppUserService(IAppUserRepository repository)
    {
        _repository = repository;
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
}