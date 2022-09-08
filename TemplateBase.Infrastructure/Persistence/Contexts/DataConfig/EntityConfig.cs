using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Infrastructure.Persistence.Contexts.DataConfig
{
    internal abstract class EntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
