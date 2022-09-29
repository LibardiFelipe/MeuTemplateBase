using Flunt.Notifications;
using MediatR;
using System.Linq;
using TemplateBase.Application.Models;

namespace TemplateBase.Application.Commands.Base
{
    public abstract class Command : Notifiable<Notification>, IRequest<Result>
    {
        public bool IsInvalid() => Notifications.Any();
        public abstract void Validate();
    }
}
