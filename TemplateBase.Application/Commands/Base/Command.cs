using Flunt.Notifications;
using MediatR;

namespace TemplateBase.Application.Commands.Base
{
    public abstract class Command : Notifiable<Notification>, IRequest<CommandResult>
    {
        public bool IsInvalid() => Notifications.Count > 0;
        public abstract void Validate();
    }
}
