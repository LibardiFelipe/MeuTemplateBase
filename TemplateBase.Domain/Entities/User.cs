using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Flunt.Notifications;
using Flunt.Validations;
using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using System.Threading;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Resources;
using System.Threading.Tasks;
using System;

namespace TemplateBase.Domain.Entities
{
    /*
     * Esse é o modelo de entidade que deverá
     * ser utilizado em todo o projeto.
    */

    public class User : Entity
    {
        #region Construtores
        public User() { }
        public User(string firebaseId, string name, string email, string? profilePictureUrl, string? id = null) : base(id)
        {
            SetFirebaseId(firebaseId);
            ChangeName(name, true);
            ChangeEmail(email, true);
            ChangeProfilePictureUrl(profilePictureUrl, true);

            IsLocked = false;
            Role = EUserRole.Customer;
        }
        #endregion

        #region Propriedades
        public string FirebaseId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? ProfilePictureUrl { get; private set; }
        public EUserRole Role { get; private set; }
        public bool IsLocked { get; private set; }
        public string? LockReason { get; private set; }
        #endregion

        #region Validações
        private void SetFirebaseId(string value)
        {
            FirebaseId = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(FirebaseId, "FirebaseId", string.Format(Mensagens.CampoObrigatorio, "FirebaseId")));
        }

        public User ChangeName(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Name?.Equals(value) ?? false))
                return this;

            Name = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Name", string.Format(Mensagens.CampoObrigatorio, "Nome")));

            return this;
        }

        public User ChangeEmail(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Email?.Equals(value) ?? false))
                return this;

            Email = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsEmail(Email, "Email", string.Format(Mensagens.CampoInvalido, "Email")));

            return this;
        }

        public User ChangeProfilePictureUrl(string? value, bool fromConstructor = false)
        {
            if (!fromConstructor && (ProfilePictureUrl?.Equals(value) ?? false))
                return this;

            ProfilePictureUrl = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsUrlOrEmpty(ProfilePictureUrl, "ProfilePicture", string.Format(Mensagens.CampoInvalido, "Foto")));

            return this;
        }

        public async Task ChangeRole(EUserRole value, CancellationToken cancellationToken)
        {
            if (Role.Equals(value))
                return;

            Role = value;
            await SetCustomClaim(this, cancellationToken);
        }

        private async Task SetCustomClaim(User user, CancellationToken cancellationToken)
        {
            try
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("path/to/serviceAccountKey.json"),
                });

                var auth = FirebaseAuth.DefaultInstance;
                var customClaims = new Dictionary<string, object>()
                {
                    { "role", $"{user.Role}" }
                };

                await auth.SetCustomUserClaimsAsync(user.FirebaseId, customClaims, cancellationToken);
            }
            catch (Exception x)
            {
                AddNotification("Role", x.Message);
            }
        }
        #endregion
    }
}
