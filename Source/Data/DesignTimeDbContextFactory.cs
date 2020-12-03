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
                                .AddJsonFile("appsettings.Development.json", true)
                                .Build();
            var connectionString = configuration.GetConnectionString("Default");
            var optionBuilder    = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionString);
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}