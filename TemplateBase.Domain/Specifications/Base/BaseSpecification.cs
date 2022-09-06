using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;

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

    public class BaseSpecification<TImplementation, TEntity> : ISpecification<TEntity> where TImplementation : BaseSpecification<TImplementation, TEntity> where TEntity : Entity
    {
        protected BaseSpecification() { }

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; private set; } = new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, object>>? OrderByAscend { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescend { get; private set; }

        public virtual TImplementation FilterById(Guid id)
        {
            CriteriaAnd((TEntity x) => x.Id == id);
            return (TImplementation)this;
        }

        protected virtual TImplementation CriteriaAnd(Expression<Func<TEntity, bool>> criteria)
        {
            if (Criteria is not null)
            {
                Expression<Func<TEntity, bool>> leftExpression = Criteria;
                Expression<Func<TEntity, bool>> rightExpression = criteria;

                var paramExpr = Expression.Parameter(typeof(TEntity));
                var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
                exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
                var finalExpr = Expression.Lambda<Func<TEntity, bool>>(exprBody, paramExpr);

                Criteria = finalExpr;
            }
            else
                Criteria = criteria;

            return (TImplementation)this;
        }

        protected virtual TImplementation CriteriaOr(Expression<Func<TEntity, bool>> criteria)
        {
            if (Criteria is not null)
            {
                Criteria = Expression.Lambda<Func<TEntity, bool>>(Expression.OrElse(Criteria, criteria));
            }
            else
                Criteria = criteria;

            return (TImplementation)this;
        }

        protected virtual TImplementation AddInclude(Expression<Func<TEntity, object>> includeCriteria)
        {
            Includes.Add(includeCriteria);
            return (TImplementation)this;
        }

        protected virtual TImplementation OrderByAscending(Expression<Func<TEntity, object>> orderByCriteria)
        {
            OrderByAscend = orderByCriteria;
            return (TImplementation)this;
        }

        protected virtual TImplementation OrderByDescending(Expression<Func<TEntity, object>> orderByCriteria)
        {
            OrderByDescend = orderByCriteria;
            return (TImplementation)this;
        }
    }
}
