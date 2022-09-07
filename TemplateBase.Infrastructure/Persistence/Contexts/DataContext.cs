using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Entities.Base;

namespace TemplateBase.Infrastructure.Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
        }
    }
}
