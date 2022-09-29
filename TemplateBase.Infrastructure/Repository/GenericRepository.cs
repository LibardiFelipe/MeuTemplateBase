using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.Specification;

namespace TemplateBase.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            if (entity.HasChanged is false)
                return;

            entity.SetNewUpdatedAt();
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var changed = new List<TEntity>();
            foreach (var entity in entities)
            {
                if (entity.HasChanged)
                {
                    entity.SetNewUpdatedAt();
                    changed.Add(entity);
                }
            }

            _dbSet.UpdateRange(changed);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return await SpecificationEvaluator<TEntity>.ApplySpecification(_dbSet.AsQueryable(), specification).ToListAsync(cancellationToken);
        }

        public async Task<bool> ContainsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return await SpecificationEvaluator<TEntity>.ApplySpecification(_dbSet.AsQueryable(), specification).AnyAsync(cancellationToken);
        }

        public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await SpecificationEvaluator<TEntity>.ApplySpecification(_dbSet.AsQueryable(), criteria).AnyAsync(cancellationToken);
        }
    }
}
