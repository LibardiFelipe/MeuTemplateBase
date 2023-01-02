using Flunt.Notifications;
using Flunt.Validations;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Domain.Classes
{
    public class NewClaim : Notifiable<Notification>
    {
        public NewClaim(string key, object value)
        {
            Key = key;
            Value = value;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Key, "Key", string.Format(Mensagens.CampoObrigatorio, "Key"))
                .IsNotNull(Value, "Value", string.Format(Mensagens.CampoObrigatorio, "Valor")));
        }

        public string Key { get; private set; }
        public object Value { get; private set; }
    }
}
