using System;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Specifications;

namespace TemplateBase.Application.Queries.Users
{
    public class UserQuery : Query<User>
    {
        #region Membros privados
        private string _name;
        private string _email;
        private EUserType? _type = null;
        private DateTime? _birthDate = null;
        #endregion

        #region Construtores
        public UserQuery() { }
        public UserQuery(string id) : base(id) { }
        #endregion

        #region Filtros
        public UserQuery FilterByName(string value)
        {
            _name = value;
            return this;
        }

        public UserQuery FilterByEmail(string value)
        {
            _email = value;
            return this;
        }

        public UserQuery FilterByType(EUserType? value)
        {
            _type = value;
            return this;
        }

        public UserQuery FilterByBirthDate(DateTime? value)
        {
            _birthDate = value;
            return this;
        }
        #endregion

        #region Specification
        public override ISpecification<User> ToSpecification()
        {
            var spec = new UserSpec();

            if (_id.HasValue)
                spec.FilterById(_id.Value);

            if (!string.IsNullOrWhiteSpace(_name))
                spec.FilterByName(_name);

            if (!string.IsNullOrWhiteSpace(_email))
                spec.FilterByEmail(_email);

            if (_type.HasValue)
                spec.FilterByType(_type.Value);

            if (_birthDate.HasValue)
                spec.FilterByBirthDate(_birthDate.Value);

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
