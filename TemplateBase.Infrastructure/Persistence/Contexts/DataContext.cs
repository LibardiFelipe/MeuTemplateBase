using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Infrastructure.Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person>? Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Entity>(x => x.Ignore(x => x.HasChanged));
            modelBuilder.Entity<Entity>(x => x.Ignore(x => x.Notifications));
        }
    }
}
