using Store.Domain.Entities;

namespace Store.Domain.Contracts;

public interface IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey, TEntity> spec, bool changeTracker = false);
    Task<TEntity?> GetAsync(TKey key);
    Task<TEntity?> GetAsync(ISpecifications<TKey, TEntity> spec, TKey key);
    Task<int> CountAsync(ISpecifications<TKey, TEntity> spec);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
