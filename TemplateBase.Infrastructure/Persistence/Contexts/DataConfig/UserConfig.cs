using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateBase.Domain.Entities;

namespace TemplateBase.Infrastructure.Persistence.Contexts.DataConfig
{
    internal class UserConfig : EntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
        }
    }
}
