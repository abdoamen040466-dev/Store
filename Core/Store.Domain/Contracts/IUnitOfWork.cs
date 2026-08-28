using Store.Domain.Entities;

namespace Store.Domain.Contracts;

public interface IUnitOfWork
{
    // Generate Repository
    public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>;
    // SaveChanges
    Task<int> SaveChangesAsync();
}
