using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Domain.Services
{
    public class UserService : Notifiable<Notification>, IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User?> CreatePersonAsync(string? name, string? email, string? password, string? profilePicture, DateTime? birthDate, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<User>();

            var entity = new User(name, email, password, profilePicture, birthDate);
            AddNotifications(entity);

            if (Notifications.Count > 0)
                return null;

            // TODO: Validar se o email já existe no banco.

            await repo!.AddAsync(entity, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return entity;

            AddNotification("", DefaultMessages.Service_InternalError);
            return null;
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Count > 0;
    }
}
