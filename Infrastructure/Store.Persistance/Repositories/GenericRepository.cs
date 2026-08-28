using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Domain.Entities.Products;
using Store.Persistance.Data.Contexts;

namespace Store.Persistance.Repositories;

public class GenericRepository<TKey, TEntity>(StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
    {
        if (typeof(TEntity) == typeof(Product))
        {
            return changeTracker ?
                await _context.Products.Include(p => p.Type).Include(p => p.Brand).ToListAsync() as IEnumerable<TEntity>
                : await _context.Products.AsNoTracking().Include(p => p.Type).Include(p => p.Brand).ToListAsync() as IEnumerable<TEntity>;
        }
        return changeTracker ?
            await _context.Set<TEntity>().ToListAsync()
            : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetAsync(TKey key)
    {
        if (typeof(TEntity) == typeof(Product))
        {
            return await _context.Products.Include(p => p.Type).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == key as int?) as TEntity;
        }
        return await _context.Set<TEntity>().FindAsync(key);
    }
    public async Task AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }
    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }


}
