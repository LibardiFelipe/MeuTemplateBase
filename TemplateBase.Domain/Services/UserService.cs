using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Domain.Services
{
    public class UserService : Notifiable<Notification>, IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Registra o usuário caso seja sua primeira conexão.
        /// </summary>
        /// <param name="claims">Lista de claims</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>"User" em caso de sucesso e "null" em caso de erros.</returns>
        public async Task<User?> SetupUserAsync(IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var repo = _uow.Repository<User>();

            var firebaseId = claims.FirstOrDefault(x => x.Type == "user_id")?.Value ?? "";
            var user = await repo.GetAsync(x => x.FirebaseId == firebaseId, cancellationToken);
            if (user is not null)
                return user;

            var name = claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "";
            var pictureURL = claims.FirstOrDefault(x => x.Type == "picture")?.Value;
            var email = "email@email.com";

            var newUser = new User(firebaseId, name, email, pictureURL);
            AddNotifications(newUser);

            if (Notifications.Any())
                return null;

            await repo.AddAsync(newUser, cancellationToken);
            if (await _uow.CommitAsync(cancellationToken) > 0)
                return newUser;

            AddNotification("", Mensagens.Service_InternalError);
            return null;
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();
    }
}
