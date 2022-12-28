using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.Specification;

namespace TemplateBase.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<bool> ContainsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ContainsAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(criteria, cancellationToken);
        }

        public async Task<bool> ContainsAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await SpecificationEvaluator<T>.ApplySpecification(_dbSet.AsQueryable(), specification).AnyAsync(cancellationToken);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);

            if (entity is not null)
                _dbSet.Remove(entity);
        }

        public async Task DeleteRange(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            var entities = await GetAllAsync(x => ids.Contains(x.Id), cancellationToken);
            foreach (var entity in entities)
                Delete(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(criteria).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await SpecificationEvaluator<T>.ApplySpecification(_dbSet.AsQueryable(), specification).ToListAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(criteria, cancellationToken);
        }

        public async Task<T?> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await SpecificationEvaluator<T>.ApplySpecification(_dbSet.AsQueryable(), specification).FirstOrDefaultAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            entity.SetNewUpdatedAt();
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.SetNewUpdatedAt();

            _dbSet.UpdateRange(entities);
        }
    }
}
