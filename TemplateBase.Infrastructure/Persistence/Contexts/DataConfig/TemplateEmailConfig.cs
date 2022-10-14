using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateBase.Domain.Entities;

namespace TemplateBase.Infrastructure.Persistence.Contexts.DataConfig
{
    internal class TemplateEmailConfig : EntityConfig<TemplateEmail>
    {
        public override void Configure(EntityTypeBuilder<TemplateEmail> builder)
        {
            builder.ToTable("TemplateEmail");
        }
    }
}
