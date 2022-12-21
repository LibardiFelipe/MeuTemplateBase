using Flunt.Notifications;
using MediatR;
using System;
using System.Linq;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Application.Queries.Base
{
    public abstract class Query<T> : Notifiable<Notification>, IRequest<Result> where T : Entity
    {
        protected Guid? _id = null;

        public Query() { }

        public Query(string id)
        {
            if (Guid.TryParse(id, out var guidId) is false)
                AddNotification("", Mensagens.Entidade_IdentificadorInvalido);

            _id = guidId;
        }

        public abstract ISpecification<T> ToSpecification();
        public bool IsInvalid() => Notifications.Any();
        public abstract void Validate();
    }
}
