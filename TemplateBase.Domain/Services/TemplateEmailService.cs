using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Domain.Services
{
    public class TemplateEmailService : Notifiable<Notification>, ITemplateEmailService
    {
        private readonly IUnitOfWork _uow;

        public TemplateEmailService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<TemplateEmail?> CreateTemplateEmailAsync(string name, string body, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<TemplateEmail>();

            var entity = new TemplateEmail(name, body);
            AddNotifications(entity);

            if (Notifications.Any())
                return null;

            await repo.AddAsync(entity, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return entity;

            AddNotification("", Mensagens.Service_InternalError);
            return null;
        }

        public async Task<TemplateEmail?> UpdateTemplateEmailAsync(Guid id, string name, string body, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<TemplateEmail>();

            var entity = await repo.GetAsync(id, cancellationToken);
            if (entity is null)
            {
                AddNotification("Id", string.Format(Mensagens.EntidadeNaoEncontrado, "Template"));
                return null;
            }

            entity.ChangeBody(body).ChangeName(name);
            AddNotifications(entity);

            if (Notifications.Any())
                return null;

            await repo.UpdateAsync(entity, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return entity;

            AddNotification("", Mensagens.Service_InternalError);
            return null;
        }

        public async Task<bool> DeleteTemplateEmailAsync(Guid id, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<TemplateEmail>();

            if (await repo.ContainsAsync(id, cancellationToken) is false)
            {
                AddNotification("Id", string.Format(Mensagens.EntidadeNaoEncontrado, "Template"));
                return false;
            }

            await repo.DeleteAsync(id, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return true;

            AddNotification("", Mensagens.Service_InternalError);
            return false;
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();

    }
}
