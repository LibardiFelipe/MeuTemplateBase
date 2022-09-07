using System;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Domain.Enumerators;

namespace TemplateBase.Application.Commands.Persons
{
    public class CreateUserCommand : Command
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? BirthDate { get; set; }

        public override void Validate()
        {
            /* 
             * Caso queira trabalhar com failfast validations,
             * aqui dentro é o local onde essas validações
             * deverão ser feitas.
            */
        }
    }
}
