using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.Users
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<SetupUserCommand, Result>
    {
        private readonly IUserService _userServices;

        public UserCommandHandler(IUserService userServices)
        {
            _userServices = userServices;
        }

        public async Task<Result> Handle(SetupUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(Mensagens.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _userServices.SetupUserAsync(request.Claims, cancellationToken);

            if (_userServices.IsInvalid())
                return new Result(Mensagens.Handler_FalhaAoExecutarComando, false, _userServices.GetNotifications());

            return new Result(Mensagens.Handler_ComandoExecutado, true, entity);
        }
    }
}
