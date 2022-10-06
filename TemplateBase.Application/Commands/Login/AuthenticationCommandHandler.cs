using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.Login
{
    public class AuthenticateCommandHandler : CommandHandler,
        IRequestHandler<AuthenticationCommand, Result>
    {
        private readonly IAuthenticationService _authService;

        public AuthenticateCommandHandler(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public async Task<Result> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _authService.AuthenticateAsync(request.Email, request.Password, cancellationToken);

            if (_authService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _authService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }
    }
}
