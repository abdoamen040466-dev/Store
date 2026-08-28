using Store.Domain.Entities;

namespace Store.Domain.Contracts;

public interface IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);
    Task<TEntity?> GetAsync(TKey key);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
