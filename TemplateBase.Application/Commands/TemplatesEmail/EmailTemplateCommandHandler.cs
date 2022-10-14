using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Application.Commands.TemplatesEmail
{
    public class TemplateEmailCommandHandler : CommandHandler,
        IRequestHandler<CreateTemplateEmailCommand, Result>,
        IRequestHandler<UpdateTemplateEmailCommand, Result>,
        IRequestHandler<DeleteEmailTemplateCommand, Result>
    {
        private readonly ITemplateEmailService _emailTemplateService;

        public TemplateEmailCommandHandler(ITemplateEmailService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }

        public async Task<Result> Handle(CreateTemplateEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _emailTemplateService.CreateTemplateEmailAsync(request.Name, request.Body, cancellationToken);

            if (_emailTemplateService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _emailTemplateService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }

        public async Task<Result> Handle(UpdateTemplateEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _emailTemplateService.UpdateTemplateEmailAsync(request.Id, request.Name, request.Body, cancellationToken);

            if (_emailTemplateService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _emailTemplateService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }

        public async Task<Result> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(DefaultMessages.Handler_ComandoInvalido, false, request.Notifications);

            var entity = await _emailTemplateService.DeleteTemplateEmailAsync(request.Id, cancellationToken);

            if (_emailTemplateService.IsInvalid())
                return new Result(DefaultMessages.Handler_FalhaAoExecutarComando, false, _emailTemplateService.GetNotifications());

            return new Result(DefaultMessages.Handler_ComandoExecutado, true, entity);
        }
    }
}
