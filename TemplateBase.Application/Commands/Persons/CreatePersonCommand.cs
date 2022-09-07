using TemplateBase.Application.Commands.Base;
using TemplateBase.Domain.Enumerators;

namespace TemplateBase.Application.Commands.Persons
{
    public class CreatePersonCommand : Command
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public byte? Age { get; set; }
        public EPersonGenre? Genre { get; set; }

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
