using Flunt.Notifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Domain.Services
{
    public class PersonService : Notifiable<Notification>, IPersonService
    {
        private readonly IUnitOfWork _uow;

        public PersonService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Person?> CreatePersonAsync(string? name, string? surname, string? email, byte? age, EPersonGenre? genre, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<Person>();

            var entity = new Person(name, surname, email, age, genre);
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
