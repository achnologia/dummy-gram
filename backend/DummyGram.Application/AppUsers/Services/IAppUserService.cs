namespace DummyGram.Application.AppUsers.Services;

public interface IAppUserService
{
    public Task<bool> UpdateAsync(string id, string displayName);
    public Task<bool> SubscribeAsync(string idUserSubscriber, string idUserSubscribeTo);
    public Task<bool> UnsubscribeAsync(string idUserSubscriber, string idUserSubscribedTo);
}