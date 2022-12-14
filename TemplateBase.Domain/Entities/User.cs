using Flunt.Notifications;
using Flunt.Validations;
using System;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Utils;

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
        public User(string name, string email, string password, string profilePictureUrl, DateTime birthDate, string id = null) : base(id)
        {
            // As propriedades já são passadas diretamente
            // para os métodos que as validam.
            // A propriedade id não necessita de uma validação aqui,
            // pois ela já ocorre dentro da classe base "Entity".
            ChangeName(name, true);
            ChangeEmail(email, true);
            ChangePassword(password, true);
            ChangeProfilePictureUrl(profilePictureUrl, true);
            ChangeBirthDate(birthDate, true);

            // Verificação por email necessária
            ChangeIsVerified(false, true);
            // Usuário sempre começa sendo o básico
            ChangeType(EUserType.Basic, true);
            ChangeIsLocked(false, true);
        }
        #endregion

        #region Propriedades
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ProfilePictureUrl { get; private set; }
        public DateTime BirthDate { get; private set; }
        public EUserType Type { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsLocked { get; private set; }
        public string LockReason { get; private set; }
        #endregion

        #region Validações
        public User ChangeName(string value, bool fromConstructor = false)
        {
            // Caso a função seja chamada de fora do construtor,
            // verifica se a propriedade que está sendo alterada é
            // igual a nova que está chegando, se for, só retorna.
            if (!fromConstructor && (Name?.Equals(value) ?? false))
                return this;

            Name = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Name", string.Format(DefaultMessages.CampoObrigatorio, "Nome")));

            // Por padrão, o repositório só salva no banco entidades
            // que tenham tido alguma alteração.
            // Essa função é responsável por marcar a entidade com
            // a flag que sinaliza que ela foi alterada.
            FlagAsChanged();
            return this;
        }

        public User ChangeIsVerified(bool value, bool fromConstructor = false)
        {
            if (!fromConstructor && IsVerified.Equals(value))
                return this;

            IsVerified = value;

            FlagAsChanged();
            return this;
        }

        public User ChangeIsLocked(bool value, bool fromConstructor = false)
        {
            if (!fromConstructor && IsLocked.Equals(value))
                return this;

            IsLocked = value;

            FlagAsChanged();
            return this;
        }

        public User ChangeLockReason(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (LockReason?.Equals(value) ?? false))
                return this;

            LockReason = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(LockReason, "LockReason", string.Format(DefaultMessages.CampoObrigatorio, "Motivo do Block")));

            FlagAsChanged();
            return this;
        }

        public User ChangeEmail(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Email?.Equals(value) ?? false))
                return this;

            Email = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsEmail(Email, "Email", string.Format(DefaultMessages.CampoInvalido, "Email")));

            FlagAsChanged();
            return this;
        }

        public User ChangePassword(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Password?.Equals(value) ?? false))
                return this;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(value, "Password", string.Format(DefaultMessages.CampoObrigatorio, "Senha")));
            // TODO: Adicionar verificação de segurança da senha

            Password = Hasher.Hash(value);

            FlagAsChanged();
            return this;
        }

        public User ChangeProfilePictureUrl(string value, bool fromConstructor = false)
        {
            if (!fromConstructor && (ProfilePictureUrl?.Equals(value) ?? false))
                return this;

            ProfilePictureUrl = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsUrlOrEmpty(ProfilePictureUrl, "ProfilePicture", string.Format(DefaultMessages.CampoInvalido, "Foto")));

            FlagAsChanged();
            return this;
        }

        public User ChangeType(EUserType value, bool fromConstructor = false)
        {
            if (!fromConstructor && Type.Equals(value))
                return this;

            Type = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Type, "Type", string.Format(DefaultMessages.CampoObrigatorio, "Permissão")));

            FlagAsChanged();
            return this;
        }

        public User ChangeBirthDate(DateTime value, bool fromConstructor = false)
        {
            if (!fromConstructor && BirthDate.Equals(value))
                return this;

            BirthDate = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsFalse(BirthDate == default, "BirthDate", string.Format(DefaultMessages.CampoObrigatorio, "Data de Nascimento")));

            FlagAsChanged();
            return this;
        }
        #endregion
    }
}
