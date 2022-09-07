using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Specifications;

namespace TemplateBase.Application.Queries.Persons
{
    public class PersonQuery : Query<Person>
    {
        #region Membros privados
        private string? _name = null;
        private string? _surname = null;
        private string? _email = null;
        private byte? _age = null;
        public EPersonGenre? _genre = null;
        #endregion

        #region Construtores
        public PersonQuery() { }
        public PersonQuery(string? id) : base(id)
        {

        }
        #endregion

        #region Filtros
        public PersonQuery FilterByName(string? value)
        {
            _name = value;
            return this;
        }

        public PersonQuery FilterBySurname(string? value)
        {
            _surname = value;
            return this;
        }

        public PersonQuery FilterByEmail(string? value)
        {
            _email = value;
            return this;
        }

        public PersonQuery FilterByAge(byte? value)
        {
            _age = value;
            return this;
        }

        public PersonQuery FilterByGenre(EPersonGenre? value)
        {
            _genre = value;
            return this;
        }
        #endregion

        #region Specification
        public override ISpecification<Person> ToSpecification()
        {
            var spec = new PersonSpec();

            if (_id.HasValue)
                spec.FilterById(_id.Value);

            if (!string.IsNullOrWhiteSpace(_name))
                spec.FilterByName(_name);

            if (!string.IsNullOrWhiteSpace(_surname))
                spec.FilterBySurname(_surname);

            if (!string.IsNullOrWhiteSpace(_email))
                spec.FilterByEmail(_email);

            if (_age.HasValue)
                spec.FilterByAge(_age.Value);

            if (_genre.HasValue)
                spec.FilterByGenre(_genre.Value);

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
