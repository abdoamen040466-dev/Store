using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities;

namespace Store.Persistance;

internal class SpecificationsEvaluator
{
    public static IQueryable<TEntity> GetQuery<Key, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<Key, TEntity> spec) where TEntity : BaseEntity<Key>
    {
        var query = inputQuery;

        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.isPagination)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        query = spec.Includes.Aggregate(query, (query, includeExpresstion) => query.Include(includeExpresstion));

        return query;
    }
}
