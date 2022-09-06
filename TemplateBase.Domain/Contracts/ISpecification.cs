using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TemplateBase.Domain.Contracts
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>> OrderByAscend { get; }
        Expression<Func<TEntity, object>> OrderByDescend { get; }
    }
}
