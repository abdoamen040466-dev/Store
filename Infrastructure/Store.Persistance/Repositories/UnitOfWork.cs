using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Persistance.Data.Contexts;
using System.Collections.Concurrent;

namespace Store.Persistance.Repositories;

public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
{
    private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();
    //private Dictionary<string, object> _repositories = new Dictionary<string, object>();

    //public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
    //{
    //    var key = typeof(TEntity).Name;
    //    if (!_repositories.ContainsKey(key))
    //    {
    //        var repository = new GenericRepository<TKey, TEntity>(_context);
    //        _repositories.Add(key, repository);
    //    }
    //    return (IGenericRepository<TKey, TEntity>)_repositories[key];
    //}
    public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
    {
        var key = typeof(TEntity).Name;
        var repo = new GenericRepository<TKey, TEntity>(_context);
        return (IGenericRepository<TKey, TEntity>)_repositories.GetOrAdd(key, repo);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
