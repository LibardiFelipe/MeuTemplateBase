using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Entities.Classes;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Domain.Specifications;

namespace TemplateBase.Domain.Services
{
    public class AuthenticationService : Notifiable<Notification>, IAuthenticationService
    {
        private readonly IUnitOfWork _uow;

        public AuthenticationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AuthData> AuthenticateAsync(string email, string password, CancellationToken cancellationToken)
        {
            var userRepo = _uow.Repository<User>();
            var user = (await userRepo.GetAllAsync(UserSpec.From(x => x.Email == email && x.Password == password), cancellationToken)).FirstOrDefault();
            if (user == null)
            {
                AddNotification("User", DefaultMessages.EmailOuSenhaIncorretos);
                return null;
            }

            return new AuthData
            {
                Name = user.Name,
                Email = user.Email,
                Permission = user.Permission,
                Token = ""
            };
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();
    }
}
