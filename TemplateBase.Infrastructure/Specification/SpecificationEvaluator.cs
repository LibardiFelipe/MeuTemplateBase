using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Infrastructure.Specification
{
    public class SpecificationEvaluator<T> where T : Entity
    {
        public static IQueryable<T> ApplySpecification(IQueryable<T> dbQuery, ISpecification<T> specification)
        {
            return GetQuery(dbQuery, specification);
        }

        public static IQueryable<T> ApplySpecification(IQueryable<T> dbQuery, Expression<Func<T, bool>> criteria)
        {
            return GetQuery(dbQuery, criteria);
        }

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
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

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, Expression<Func<T, bool>> criteria)
        {
            var query = inputQuery;

            if (criteria is not null)
                query = query.Where(criteria);

            return query;
        }
    }
}
