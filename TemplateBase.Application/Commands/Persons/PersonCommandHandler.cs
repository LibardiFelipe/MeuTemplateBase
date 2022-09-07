using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.Persons
{
    public class PersonCommandHandler : CommandHandler,
        IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IPersonService _personService;

        public PersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new CommandResult(DefaultMessages.Handler_ComandoInvalido, false);

            var entity = await _personService.CreatePersonAsync(request.Name, request.Surname,
                request.Email,request.Age, request.Genre, cancellationToken);

            if (_personService.IsInvalid())
                return new CommandResult(DefaultMessages.Handler_FalhaAoExecutarComando, false, _personService.GetNotifications());

            return new CommandResult(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }
    }
}
