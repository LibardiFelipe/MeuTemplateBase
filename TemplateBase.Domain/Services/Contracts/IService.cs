using Flunt.Notifications;
using System.Collections.Generic;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IService
    {
        IReadOnlyCollection<Notification> GetNotifications();
        bool IsInvalid();
    }
}
