using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseMigrator
    {
        public DatabaseMigrator()
        {
            var dbContextFactory = new DesignTimeDbContextFactory();
            var dbContext        = dbContextFactory.CreateDbContext(Array.Empty<string>());
            if (dbContext.Database.GetPendingMigrations().Any()) dbContext.Database.Migrate();
        }
    }
}