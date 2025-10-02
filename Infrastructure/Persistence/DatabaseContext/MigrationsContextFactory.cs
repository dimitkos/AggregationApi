using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.DatabaseContext
{
    class MigrationsContextFactory : IDesignTimeDbContextFactory<AggreegationDbContext>
    {
        public AggreegationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../Api/"))
                .AddJsonFile("appsettings.json")
                //.AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var builder = new DbContextOptionsBuilder<AggreegationDbContext>();
            builder.UseSqlite(configuration.GetConnectionString(Constants.Databases.Aggregation));

            return new AggreegationDbContext(builder.Options);
        }
    }
}
