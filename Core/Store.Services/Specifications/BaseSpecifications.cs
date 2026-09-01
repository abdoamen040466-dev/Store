using Store.Domain.Contracts;
using Store.Domain.Entities;
using System.Linq.Expressions;

namespace Store.Services.Specifications;

public class BaseSpecifications<Key, TEntity> : ISpecifications<Key, TEntity> where TEntity : BaseEntity<Key>
{
    public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
    public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    public Expression<Func<TEntity, object>>? OrderBy { get; set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool isPagination { get; set; }

    public BaseSpecifications(Expression<Func<TEntity, bool>>? expresstion)
    {
        Criteria = expresstion;
    }

    public void ApplyPagination(int PageSize, int PageIndex)
    {
        Skip = (PageIndex - 1) * PageSize;
        Take = PageSize;
        isPagination = true;
    }

    public void AddOrderBy(Expression<Func<TEntity, object>>? expression)
    {
        OrderBy = expression;
    }
    public void AddOrderByDescending(Expression<Func<TEntity, object>>? expression)
    {
        OrderByDescending = expression;
    }
}
