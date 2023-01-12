using Flunt.Notifications;
using System;
using System.Linq;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Domain.Entities.Base
{
    public abstract class Entity : Notifiable<Notification>
    {
        protected Entity(string? id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid();
            else
            {
                if (Guid.TryParse(id, out var guidId) is false)
                    AddNotification("", Mensagens.Entidade_IdentificadorInvalido);

                Id = guidId;
            }

            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected void SetNewCreatedAt(DateTime? newCreatedAt = null) => CreatedAt = newCreatedAt ?? DateTime.Now;
        public void SetNewUpdatedAt(DateTime? newUpdatedAt = null) => UpdatedAt = newUpdatedAt ?? DateTime.Now;
        public bool IsInvalid() => Notifications.Any();
    }
}
