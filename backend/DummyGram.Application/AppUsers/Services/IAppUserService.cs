using DummyGram.Domain.Entities;

namespace DummyGram.Application.AppUsers.Services;

public interface IAppUserService
{
    public Task<bool> UpdateAsync(string id, string displayName);
    public Task<bool> SubscribeAsync(string idUserSubscriber, string idUserSubscribeTo);
    public Task<bool> UnsubscribeAsync(string idUserSubscriber, string idUserSubscribedTo);
    public Task<bool> SavePostAsync(string id, int idPost);
    public Task<bool> RemoveSavedPostAsync(string id, int idPost);
    public Task<AppUser> GetUser(string id);
}