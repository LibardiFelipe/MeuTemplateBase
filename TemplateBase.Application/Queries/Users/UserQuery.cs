using System.Collections.Generic;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Specifications;

namespace TemplateBase.Application.Queries.Users
{
    public class UserQuery : Query<UserQuery, User>
    {
        #region Membros privados
        private string? _name = null;
        private string? _email = null;
        private EUserType? _type = null;
        #endregion

        #region Construtores
        public UserQuery() { }
        public UserQuery(string id) : base(id) { }
        #endregion

        #region Filtros
        public UserQuery FilterByName(string? value)
        {
            _name = value;
            return this;
        }

        public UserQuery FilterByEmail(string? value)
        {
            _email = value;
            return this;
        }

        public UserQuery FilterByType(EUserType? value)
        {
            _type = value;
            return this;
        }
        #endregion

        #region Specification
        public override ISpecification<User> ToSpecification()
        {
            var spec = new UserSpec();

            if (_id.HasValue)
                spec.FilterById(_id.Value);

            if (_createdAtStart.HasValue && _createdAtEnd.HasValue)
                spec.FilterByCreationRange(_createdAtStart.Value, _createdAtEnd.Value);

            if (_updatedAtStart.HasValue && _updatedAtEnd.HasValue)
                spec.FilterByUpdateRange(_updatedAtStart.Value, _updatedAtEnd.Value);

            if (!string.IsNullOrWhiteSpace(_name))
                spec.FilterByName(_name);

            if (!string.IsNullOrWhiteSpace(_email))
                spec.FilterByEmail(_email);

            if (_type.HasValue)
                spec.FilterByType(_type.Value);

            return spec;
        }
        #endregion

        #region Validações
        public override void Validate()
        {
            
        }
        #endregion
    }
}
