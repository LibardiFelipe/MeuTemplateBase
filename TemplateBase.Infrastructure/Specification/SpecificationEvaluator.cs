using System.Linq;
using TemplateBase.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Infrastructure.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : Entity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Includes is not null)
                query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.OrderByAscend is not null)
                query = query.OrderBy(specification.OrderByAscend);
            else if (specification.OrderByDescend is not null)
                query = query.OrderByDescending(specification.OrderByDescend);

            return query;
        }
    }
}
