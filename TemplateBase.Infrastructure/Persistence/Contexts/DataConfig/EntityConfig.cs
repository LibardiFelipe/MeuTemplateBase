using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Infrastructure.Persistence.Contexts.DataConfig
{
    internal abstract class EntityConfig<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
