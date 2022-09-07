using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Application.Queries.Persons;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Application.Queries
{
    public class QueryHandler : IRequestHandler<PersonQuery, QueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<QueryResult> Handle(PersonQuery request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new QueryResult(DefaultMessages.Handler_QueryInvalida, false, request.Notifications);

            var repo = _unitOfWork.Repository<Person>();
            var spec = request.ToSpecification();
            var result = await repo!.GetAllAsync(spec, cancellationToken);

            return new QueryResult(DefaultMessages.Handler_QueryExecutada, true, result);
        }
    }
}
