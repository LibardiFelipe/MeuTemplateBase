using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Domain.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        Task<bool> ContainsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
