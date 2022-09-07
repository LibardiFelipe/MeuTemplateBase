using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.Persons
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<CreateUserCommand, CommandResult>
    {
        private readonly IUserService _personService;

        public UserCommandHandler(IUserService personService)
        {
            _personService = personService;
        }

        public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new CommandResult(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _personService.CreatePersonAsync(request.Name, request.Email,
                request.Password, request.ProfilePictureUrl, request.BirthDate, cancellationToken);

            if (_personService.IsInvalid())
                return new CommandResult(DefaultMessages.Handler_FalhaAoExecutarComando, false, _personService.GetNotifications());

            return new CommandResult(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }
    }
}
