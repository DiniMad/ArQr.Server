using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseMigrator : IAsyncDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseMigrator()
        {
            var dbContextFactory = new DesignTimeDbContextFactory();
            _dbContext = dbContextFactory.CreateDbContext(Array.Empty<string>());
            if (_dbContext.Database.GetPendingMigrations().Any()) _dbContext.Database.Migrate();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}