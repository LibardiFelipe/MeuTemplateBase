using System;
using System.Linq;
using System.Linq.Expressions;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Domain.Contracts
{
    public interface ISpecificationEvaluator<T> where T : Entity
    {
        static abstract IQueryable<T> ApplySpecification(IQueryable<T> dbQuery, ISpecification<T> specification);
        static abstract IQueryable<T> ApplySpecification(IQueryable<T> dbQuery, Expression<Func<T, bool>> criteria);
        static abstract IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification);
        static abstract IQueryable<T> GetQuery(IQueryable<T> inputQuery, Expression<Func<T, bool>> criteria);
    }
}
