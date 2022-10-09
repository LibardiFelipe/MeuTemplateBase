using System;
using System.Linq.Expressions;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Specifications.Base;

namespace TemplateBase.Domain.Specifications
{
    public class TemplateEmailSpec : BaseSpecification<TemplateEmailSpec, TemplateEmail>
    {
        public TemplateEmailSpec() { }
        private TemplateEmailSpec(Expression<Func<TemplateEmail, bool>> criteria) : base(criteria) { }

        public static TemplateEmailSpec From(Expression<Func<TemplateEmail, bool>> criteria) => new(criteria);
    }
}
