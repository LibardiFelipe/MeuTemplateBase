using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TemplateBase.Domain.Contracts
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderByAscend { get; }
        Expression<Func<T, object>> OrderByDescend { get; }
    }
}
