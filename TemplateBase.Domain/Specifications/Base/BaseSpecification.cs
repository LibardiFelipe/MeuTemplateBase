using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Domain.Specifications.Base
{
    internal class ParameterReplacer : ExpressionVisitor
    {

        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(_parameter);

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }

    public class BaseSpecification<TImplementation, T> : ISpecification<T> where TImplementation : BaseSpecification<TImplementation, T> where T : Entity
    {
        protected BaseSpecification() { }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderByAscend { get; private set; }

        public Expression<Func<T, object>> OrderByDescend { get; private set; }

        public virtual TImplementation FilterById(Guid id)
        {
            CriteriaAnd((T x) => x.Id == id);
            return (TImplementation)this;
        }

        protected virtual TImplementation CriteriaAnd(Expression<Func<T, bool>> criteria)
        {
            if (Criteria is not null)
            {
                Expression<Func<T, bool>> leftExpression = Criteria;
                Expression<Func<T, bool>> rightExpression = criteria;

                var paramExpr = Expression.Parameter(typeof(T));
                var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
                exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
                var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

                Criteria = finalExpr;
            }
            else
                Criteria = criteria;

            return (TImplementation)this;
        }

        protected virtual TImplementation CriteriaOr(Expression<Func<T, bool>> criteria)
        {
            if (Criteria is not null)
            {
                Criteria = Expression.Lambda<Func<T, bool>>(Expression.OrElse(Criteria, criteria));
            }
            else
                Criteria = criteria;

            return (TImplementation)this;
        }

        protected virtual TImplementation AddInclude(Expression<Func<T, object>> includeCriteria)
        {
            Includes.Add(includeCriteria);
            return (TImplementation)this;
        }

        protected virtual TImplementation OrderByAscending(Expression<Func<T, object>> orderByCriteria)
        {
            OrderByAscend = orderByCriteria;
            return (TImplementation)this;
        }

        protected virtual TImplementation OrderByDescending(Expression<Func<T, object>> orderByCriteria)
        {
            OrderByDescend = orderByCriteria;
            return (TImplementation)this;
        }
    }
}
