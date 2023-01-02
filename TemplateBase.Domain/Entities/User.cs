using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Flunt.Notifications;
using Flunt.Validations;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Classes;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Resources;

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
        #endregion

        #region Métodos públicos
        public async Task ChangeRole(EUserRole value, CancellationToken cancellationToken)
        {
            if (Role.Equals(value))
                return;

            Role = value;
            var claim = new NewClaim("role", $"{Role}");
            AddNotifications(claim);

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsBetween((byte)Role, (byte)EUserRole.Customer, (byte)EUserRole.Admin, "Role", "A role adicionada não é válida!"));

            if (IsInvalid())
                return;

            await SetCustomClaim(FirebaseId, claim, cancellationToken);
        }
        #endregion

        #region Métodos privados
        private async Task SetCustomClaim(string firebaseId, NewClaim claim, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(firebaseId))
                AddNotification("FirebaseId", string.Format(Mensagens.CampoObrigatorio, "FirebaseId"));

            if (IsInvalid())
                return;

            try
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(new MyGoogleCredential())),
                });

                var auth = FirebaseAuth.DefaultInstance;

                var newClaim = new Dictionary<string, object>()
                {
                    { claim.Key, $"{claim.Value}" }
                };

                await auth.SetCustomUserClaimsAsync(firebaseId, newClaim, cancellationToken);
            }
            catch (Exception x)
            {
                AddNotification("AddClaim", x.Message);
            }
        }

        private async Task RemoveCustomClaim(string firebaseId, string claimKey, CancellationToken cancellationToken)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(firebaseId, "FirebaseId", string.Format(Mensagens.CampoObrigatorio, "FirebaseId"))
                .IsNotNullOrWhiteSpace(claimKey, "ClaimKey", string.Format(Mensagens.CampoObrigatorio, "ClaimKey")));

            if (IsInvalid())
                return;

            try
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(new MyGoogleCredential())),
                });

                var auth = FirebaseAuth.DefaultInstance;

                var newClaim = new Dictionary<string, object>()
                {
                    { claimKey, null! }
                };

                await auth.SetCustomUserClaimsAsync(firebaseId, newClaim, cancellationToken);
            }
            catch (Exception x)
            {
                AddNotification("RemoveClaim", x.Message);
            }
        }
        #endregion
    }
}
