using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TZ_Infotecs_Winter_2026.Domain.Entities;

namespace TZ_Infotecs_Winter_2026.Infrastructure.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }
        public DbSet<Value> Values { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
