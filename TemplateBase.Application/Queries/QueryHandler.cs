using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Application.Models;
using TemplateBase.Application.Queries.TemplatesEmail;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Resources;

namespace TemplateBase.Application.Queries
{
    public class QueryHandler :
        IRequestHandler<TemplateEmailQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(TemplateEmailQuery request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid())
                return new Result(Mensagens.Handler_QueryInvalida, false, request.Notifications);

            var repo = _unitOfWork.Repository<TemplateEmail>();
            var spec = request.ToSpecification();
            var result = await repo.GetAllAsync(spec, cancellationToken);

            return new Result(Mensagens.Handler_QueryExecutada, true, result);
        }
    }
}
