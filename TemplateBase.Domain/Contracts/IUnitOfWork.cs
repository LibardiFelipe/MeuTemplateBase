using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
