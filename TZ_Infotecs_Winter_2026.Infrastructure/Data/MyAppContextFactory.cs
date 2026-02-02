using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TZ_Infotecs_Winter_2026.Infrastructure.Data
{
    internal sealed class MyAppContextFactory : IDesignTimeDbContextFactory<MyAppContext>
    {
        public MyAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyAppContext>();
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../TZ_Infotecs_Winter_2026.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString,
                m => m.MigrationsAssembly("TZ_Infotecs_Winter_2026.Infrastructure"));
            
            return new MyAppContext(optionsBuilder.Options);
        }
    }
}
