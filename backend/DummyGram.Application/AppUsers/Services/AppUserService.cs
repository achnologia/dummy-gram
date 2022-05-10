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
}