using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.Persons
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserService _personService;

        public UserCommandHandler(IUserService personService)
        {
            _personService = personService;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _personService.CreateUserAsync(request.Name, request.Email,
                request.Password, request.ProfilePictureUrl, request.BirthDate, cancellationToken);

            if (_personService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _personService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }
    }
}
