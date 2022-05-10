namespace DummyGram.Application.AppUsers.Services;

public interface IAppUserService
{
    public Task<bool> UpdateAsync(string id, string displayName);
}