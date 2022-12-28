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
        Task<bool> ContainsAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<T?> GetAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken);
        Task<T?> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task Delete(Guid id, CancellationToken cancellationToken);
        void Delete(T entity);
        Task DeleteRange(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
