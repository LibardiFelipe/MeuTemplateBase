using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : Entity;
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
