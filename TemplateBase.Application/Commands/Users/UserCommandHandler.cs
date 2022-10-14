using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Auth;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.Persons
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<RegisterUserCommand, Result>,
        IRequestHandler<VerifyUserCommand, Result>
    {
        private readonly IUserService _userService;

        public UserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _userService.RegisterUserAsync(request.Name, request.Email,
                request.Password, request.BirthDate, request.ProfilePicture, cancellationToken);

            if (_userService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _userService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }

        public async Task<Result> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _userService.VerifyUserAsync(request.Hash, cancellationToken);

            if (_userService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _userService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }
    }
}
