using Flunt.Notifications;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private readonly string _confirmationSecret = string.Empty;
        private readonly string _confirmationRoute = string.Empty;

        public UserService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
            _confirmationSecret = _configuration.GetValue<string>("EmailConfirmationSecret");
            _confirmationRoute = _configuration.GetValue<string>("EmailConfirmationRoute");
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password, DateTime birthDate, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<User>();
            var repoTemplates = _uow.Repository<TemplateEmail>();

            // TODO: Adicionar serviço de upload de imagem e setar a url
            var entity = new User(name, email, password, "", birthDate);
            AddNotifications(entity);

            bool exists = await repo.ContainsAsync(UserSpec.From(x => x.Email == email), cancellationToken);
            if (exists)
                AddNotification("Email", DefaultMessages.EmailJaExistente);

            if (Notifications.Any())
                return null;

            var template = await repoTemplates.GetByIdAsync(Guid.Parse("7154d2ee-4f0f-4ce0-9495-f2096060a784"), cancellationToken);
            if (template is null)
            {
                AddNotification("", "Template de email não encontrado.");
                return null;
            }

            SendVerificationEmail(entity, template.Body);

            await repo.AddAsync(entity, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return entity;

            AddNotification("", DefaultMessages.Service_InternalError);
            return null;
        }

        public async Task<bool> VerifyUserAsync(string hash, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<User>();

            var idString = Crypt.DecryptString(_confirmationSecret, hash);
            if (Guid.TryParse(idString, out var idGuid) is false)
            {
                AddNotification("", "Hash inválida!");
                return false;
            }

            var user = await repo.GetByIdAsync(idGuid, cancellationToken);
            if (user is null)
            {
                AddNotification("", "Hash inválida!");
                return false;
            }

            if (user.IsVerified)
                return true;

            user.ChangeIsVerified(true);

            repo.Update(user);

            if (await _uow.CommitAsync(cancellationToken) > 0)
                return true;

            AddNotification("", DefaultMessages.Service_InternalError);
            return false;
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();

        #region Privados
        private bool SendVerificationEmail(User user, string emailBody)
        {
            var emailConfig = new EmailConfig
            {
                User = _configuration.GetValue<string>("Email:User"),
                Password = _configuration.GetValue<string>("Email:Password"),
                Address = _configuration.GetValue<string>("Email:Address"),
                Port = _configuration.GetValue<int>("Email:Port"),
                EnableSsl = _configuration.GetValue<bool>("Email:EnableSsl"),
            };

            var replaces = new Dictionary<string, string>
            {
                { "@user", user.Name.Split(' ').FirstOrDefault() ?? "usuário" },
                { "@confirmationRoute", _configuration.GetValue<string>("EmailConfirmationRoute") },
                { "@confirmationHash", Crypt.EncryptString(_confirmationSecret, user.Id.ToString()) }
            };

            var emailToSend = new Email
            {
                Body = emailBody,
                Subject = "Confirmação de Cadastro"
            };
            emailToSend.InjectToBody(replaces);

            emailToSend.AddAddressee(user.Email);
            return EmailService.Send(emailConfig, emailToSend);
        }
        #endregion
    }
}
