using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Specifications;

namespace TemplateBase.Application.Queries.TemplatesEmail
{
    public class TemplateEmailQuery : Query<TemplateEmail>
    {
        #region Construtores
        public TemplateEmailQuery() { }
        public TemplateEmailQuery(string id) : base(id) { }
        #endregion

        #region Specification
        public override ISpecification<TemplateEmail> ToSpecification()
        {
            var spec = new TemplateEmailSpec();

            if (_id.HasValue)
                spec.FilterById(_id.Value);

            return spec;
        }
        #endregion

        #region Validações
        public override void Validate()
        {
            /* 
             * Caso queira trabalhar com failfast validations,
             * aqui dentro é o local onde essas validações
             * deverão ser feitas.
            */
        }
        #endregion
    }
}
