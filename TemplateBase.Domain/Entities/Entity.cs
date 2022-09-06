using Flunt.Notifications;
using System;

namespace TemplateBase.Domain.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Entity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected void SetNewCreatedAt(DateTime? newCreatedAt = null) => CreatedAt = newCreatedAt ?? DateTime.Now;
        public void SetNewUpdatedAt(DateTime? newUpdatedAt = null) => UpdatedAt = newUpdatedAt ?? DateTime.Now;
    }
}
