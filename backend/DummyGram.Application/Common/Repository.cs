using DummyGram.Domain.Entities;
using DummyGram.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DummyGram.Application.Common;

public class Repository<TEntity> : IRepository<TEntity> 
    where TEntity: Entity
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(TEntity entityToUpdate)
    {
        _context.Set<TEntity>().Update(entityToUpdate);
        var updated = await _context.SaveChangesAsync();

        return updated > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            return false;

        _context.Set<TEntity>().Remove(entity);
        var deleted = await _context.SaveChangesAsync();

        return deleted > 0;
    }
    
    public async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);

        return entity;
    }
    
    public async Task<TEntity> GetByIdNoTrackingAsync(int id)
    {
        var entity = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        return entity;
    }
}