using DummyGram.Domain.Entities;

namespace DummyGram.Application.Common;

public interface IRepository<TEntity> 
    where TEntity: Entity
{
    public Task AddAsync(TEntity entity);
    public Task<bool> UpdateAsync(TEntity entityToUpdate);
    public Task<bool> DeleteAsync(int id);
    public Task<TEntity> GetByIdAsync(int id);
    public Task<TEntity> GetByIdNoTrackingAsync(int id);
}