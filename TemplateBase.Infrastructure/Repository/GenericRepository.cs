using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using TemplateBase.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using TemplateBase.Infrastructure.Specification;
using TemplateBase.Infrastructure.Persistence.Contexts;
using System.Threading;
using TemplateBase.Domain.Entities.Base;

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

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);
        }

        public async Task<bool> ContainsIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
