using DummyGram.Domain.Entities;
using DummyGram.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DummyGram.Application.AppUsers.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly ApplicationDbContext _context;

    public AppUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser> GetByIdAsync(string id)
    {
        var appUser = await _context.AppUsers.SingleOrDefaultAsync(x => x.Id == id);

        return appUser;
    }

    public async Task<bool> UpdateAsync(AppUser user)
    {
        _context.AppUsers.Update(user);
        var updated = await _context.SaveChangesAsync();

        return updated > 0;
    }
}