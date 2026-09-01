using Store.Domain.Entities;
using System.Linq.Expressions;

namespace Store.Domain.Contracts;

public interface ISpecifications<Key, TEntity> where TEntity : BaseEntity<Key>
{
    List<Expression<Func<TEntity, object>>> Includes { get; set; }
    Expression<Func<TEntity, bool>>? Criteria { get; set; }
    Expression<Func<TEntity, Object>>? OrderBy { get; set; }
    Expression<Func<TEntity, Object>>? OrderByDescending { get; set; }
    int Skip { get; set; }
    int Take { get; set; }
    bool isPagination { get; set; }
}
