using Flunt.Notifications;
using MediatR;
using TemplateBase.Application.Models;

namespace TemplateBase.Application.Commands.Base
{
    public abstract class Command : Notifiable<Notification>, IRequest<Result>
    {
        public bool IsInvalid() => Notifications.Count > 0;
        public abstract void Validate();
    }
}
