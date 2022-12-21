using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.Repository;

namespace TemplateBase.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;
        public Dictionary<Type, object> Repositories = new();

        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            if (Repositories.ContainsKey(typeof(TEntity)) == true)
                return (Repositories[typeof(TEntity)] as IGenericRepository<TEntity>)!;

            IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(_dbContext);
            Repositories.Add(typeof(TEntity), repo);
            return repo;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
