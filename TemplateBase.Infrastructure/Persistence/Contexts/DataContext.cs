using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.Infrastructure.Persistence.Contexts.DataConfig;

namespace TemplateBase.Infrastructure.Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TemplateEmail> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityConfig<>).Assembly);

            modelBuilder.Ignore<Entity>();
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Notifiable<Notification>>();
        }
    }
}
