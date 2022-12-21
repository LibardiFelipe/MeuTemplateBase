using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Domain.Contracts
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<bool> ContainsAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ContainsAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken);

        Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<T?> GetAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken);
        Task<T?> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    }
}
