using TemplateBase.Domain.Contracts;

namespace TemplateBase.Application.Queries
{
    public class QueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
