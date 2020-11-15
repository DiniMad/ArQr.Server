using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var connectionString = args.Length > 0 ? args[0] : configuration.GetConnectionString("Default");

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionString);
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}