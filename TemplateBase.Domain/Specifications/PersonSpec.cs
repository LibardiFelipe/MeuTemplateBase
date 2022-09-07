using System;
using System.Linq.Expressions;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Specifications.Base;

namespace TemplateBase.Domain.Specifications
{
    public class PersonSpec : BaseSpecification<PersonSpec, Person>
    {
        public PersonSpec() { }
        private PersonSpec(Expression<Func<Person, bool>> criteria) : base(criteria) { }

        public PersonSpec FilterByName(string value)
        {
            CriteriaAnd(x => x.Name != null && x.Name.ToLower().Contains(value.ToLower()));
            return this;
        }

        public PersonSpec FilterBySurname(string value)
        {
            CriteriaAnd(x => x.Surname != null && x.Surname.ToLower().Contains(value.ToLower()));
            return this;
        }

        public PersonSpec FilterByEmail(string value)
        {
            CriteriaAnd(x => x.Email != null && x.Email.ToLower().Contains(value.ToLower()));
            return this;
        }

        public PersonSpec FilterByAge(byte value)
        {
            CriteriaAnd(x => x.Age != null && x.Age == value);
            return this;
        }

        public PersonSpec FilterByGenre(EPersonGenre value)
        {
            CriteriaAnd(x => x.Genre != null && x.Genre == value);
            return this;
        }

        public static PersonSpec From(Expression<Func<Person, bool>> criteria) => new(criteria);
    }
}
