using DummyGram.Domain.Entities;

namespace DummyGram.Application.AppUsers.Repositories;

public interface IAppUserRepository
{
    public Task<AppUser> GetByIdAsync(string id);
    public Task<AppUser> GetByIdNoTrackingAsync(string id);
    public Task<bool> UpdateAsync(AppUser user);
}