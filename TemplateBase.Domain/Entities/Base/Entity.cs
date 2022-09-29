using Flunt.Notifications;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Domain.Entities.Base
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Entity(string id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid();
            else
            {
                if (Guid.TryParse(id, out var guidId) is false)
                    AddNotification("", DefaultMessages.Entidade_IdentificadorInvalido);

                Id = guidId;
            }

            CreatedAt = DateTime.Now;
            HasChanged = false;
        }

        public Guid Id { get; private set; }
        [NotMapped]
        public bool HasChanged { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected void SetNewCreatedAt(DateTime? newCreatedAt = null) => CreatedAt = newCreatedAt ?? DateTime.Now;
        public void SetNewUpdatedAt(DateTime? newUpdatedAt = null) => UpdatedAt = newUpdatedAt ?? DateTime.Now;
        protected void FlagAsChanged() => HasChanged = true;
        public bool IsInvalid() => Notifications.Count > 0;
    }
}
