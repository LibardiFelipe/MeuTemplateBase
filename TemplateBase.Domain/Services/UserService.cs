using Flunt.Notifications;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Classes;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Domain.Specifications;
using TemplateBase.Domain.Utils;

namespace TemplateBase.Domain.Services
{
    public class UserService : Notifiable<Notification>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password, string profilePicture, DateTime birthDate, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<User>();

            var entity = new User(name, email, password, profilePicture, birthDate);
            AddNotifications(entity);

            bool exists = await repo.ContainsAsync(UserSpec.From(x => x.Email == email), cancellationToken);
            if (exists)
                AddNotification("Email", DefaultMessages.EmailJaExistente);

            if (Notifications.Any())
                return null;

            SendVerificationEmail(email);

            await repo.AddAsync(entity, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return entity;

            AddNotification("", DefaultMessages.Service_InternalError);
            return null;
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();

        #region Privados
        private void SendVerificationEmail(string email)
        {
            var emailConfig = new EmailConfig
            {
                User = _configuration.GetValue<string>("Email:User"),
                Password = _configuration.GetValue<string>("Email:Password"),
                Address = _configuration.GetValue<string>("Email:Address"),
                Port = _configuration.GetValue<int>("Email:Port"),
                EnableSsl = _configuration.GetValue<bool>("Email:EnableSsl"),
            };

            // TODO: Gerar uma hash de confirmação para injetar no body
            var replaces = new Dictionary<string, string>
            {
                { "confirmationHash", "HASHQUESERÁGERADAPROUSUÁRIOCONFIRMAROEMAIL" }
            };

            var emailToSend = new Email
            {
                Body = "<b>Email body in html {confirmationHash}</b>", // TODO: Buscar o template direto do banco
                Subject = "Email Subject"
            };
            emailToSend.InjectToBody(replaces);

            emailToSend.AddAddressee(email);
            EmailService.Send(emailConfig, emailToSend);
        }
        #endregion
    }
}
