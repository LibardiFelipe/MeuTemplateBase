using Flunt.Notifications;
using Flunt.Validations;
using System;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Domain.Enumerators;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Domain.Entities
{
    /*
     * Esse é o modelo de entidade que deverá
     * ser utilizado em todo o projeto.
    */

    public class Pessoa : Entity
    {
        #region Construtores
        public Pessoa() { }

        public Pessoa(string? name, string? surname, string? email, byte? age, EGeneroPessoa? genre, string? id = null) : base(id)
        {
            // As propriedades já são passadas diretamente
            // para os métodos que as validam.
            // A propriedade id não necessita de uma validação aqui,
            // pois ela já ocorre dentro da classe base "Entity".
            ChangeName(name, true);
            ChangeSurname(surname, true);
            ChangeEmail(email, true);
            ChangeAge(age, true);
            ChangeGenre(genre, true);
        }
        #endregion

        #region Propriedades
        /*
         * Todas as propriedades são declaradas como nullable.
         * A ideia é evitar exceptions de banco e tratar as
         * obrigatoriedades através de validações no código.
        */
        public string? Name { get; private set; }
        public string? Surname { get; private set; }
        public string? Email { get; private set; }
        public byte? Age { get; private set; }
        public EGeneroPessoa? Genre { get; private set; }
        #endregion

        #region Validações
        public Pessoa ChangeName(string? value, bool fromConstructor = false)
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

        public Pessoa ChangeSurname(string? value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Surname?.Equals(value) ?? false))
                return this;

            Surname = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Surname, "Surname", string.Format(DefaultMessages.CampoObrigatorio, "Sobrenome")));

            FlagAsChanged();
            return this;
        }

        public Pessoa ChangeEmail(string? value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Email?.Equals(value) ?? false))
                return this;

            Email = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Email, "Email", string.Format(DefaultMessages.CampoObrigatorio, "Email")));

            FlagAsChanged();
            return this;
        }

        public Pessoa ChangeAge(byte? value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Age?.Equals(value) ?? false))
                return this;

            Age = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Age, "Age", string.Format(DefaultMessages.CampoObrigatorio, "Idade")));

            FlagAsChanged();
            return this;
        }

        public Pessoa ChangeGenre(EGeneroPessoa? value, bool fromConstructor = false)
        {
            if (!fromConstructor && (Genre?.Equals(value) ?? false))
                return this;

            Genre = value;
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Genre, "Genre", string.Format(DefaultMessages.CampoObrigatorio, "Gênero")));

            FlagAsChanged();
            return this;
        }
        #endregion
    }
}
