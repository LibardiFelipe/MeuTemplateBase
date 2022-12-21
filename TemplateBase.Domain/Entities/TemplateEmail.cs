using Flunt.Notifications;
using Flunt.Validations;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Domain.Entities
{
    public class TemplateEmail : Entity
    {
        public TemplateEmail() { }
        public TemplateEmail(string name, string body, string? id = null) : base(id)
        {
            ChangeName(name, true);
            ChangeBody(body, true);
        }

        public string Name { get; private set; }
        public string Body { get; private set; }

        public TemplateEmail ChangeName(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Name?.Equals(value) ?? false))
                return this;

            Name = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Name", string.Format(Mensagens.CampoObrigatorio, "Nome")));

            return this;
        }

        public TemplateEmail ChangeBody(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Body?.Equals(value) ?? false))
                return this;

            Body = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Body, "Body", string.Format(Mensagens.CampoObrigatorio, "Corpo")));

            return this;
        }
    }
}
