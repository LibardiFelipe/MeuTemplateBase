using System;
using System.Linq.Expressions;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Specifications.Base;

namespace TemplateBase.Domain.Specifications
{
    public class UserSpec : BaseSpecification<UserSpec, User>
    {
        public UserSpec() { }
        private UserSpec(Expression<Func<User, bool>> criteria) : base(criteria) { }

        public UserSpec FilterByName(string value)
        {
            CriteriaAnd(x => x.Name.ToLower().Contains(value.ToLower()));
            return this;
        }

        public UserSpec FilterByEmail(string value)
        {
            CriteriaAnd(x => x.Email.ToLower().Contains(value.ToLower()));
            return this;
        }

        public UserSpec FilterByPermission(EUserPermission value)
        {
            CriteriaAnd(x => x.Permission == value);
            return this;
        }

        public UserSpec FilterByBirthDate(DateTime value)
        {
            CriteriaAnd(x => x.BirthDate == value.Date);
            return this;
        }

        public static UserSpec From(Expression<Func<User, bool>> criteria) => new(criteria);
    }
}
