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
    public abstract class Query<TQuery, TEntity> : Notifiable<Notification>, IRequest<Result> where TEntity : Entity where TQuery : Query<TQuery, TEntity>
    {
        protected Guid? _id = null;
        protected DateTime? _createdAtStart = null;
        protected DateTime? _createdAtEnd = null;
        protected DateTime? _updatedAtStart = null;
        protected DateTime? _updatedAtEnd = null;

        public Query() { }

        public Query(string id)
        {
            if (Guid.TryParse(id, out var guidId) is false)
                AddNotification("", Mensagens.Entidade_IdentificadorInvalido);

            _id = guidId;
        }

        public TQuery FilterByCreationRange(DateTime? startDate, DateTime? endDate)
        {
            _createdAtStart = startDate;
            _createdAtEnd = endDate;
            return (TQuery)this;
        }

        public TQuery FilterByUpdateRange(DateTime? startDate, DateTime? endDate)
        {
            _updatedAtStart = startDate;
            _updatedAtEnd = endDate;
            return (TQuery)this;
        }

        public abstract ISpecification<TEntity> ToSpecification();
        public bool IsInvalid() => Notifications.Any();
        public abstract void Validate();
    }
}
