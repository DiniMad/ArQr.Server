using System;
using System.Threading.Tasks;
using Data.Repository.Base;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext DbContext      { get; }
        public  IUserRepository      UserRepository { get; }

        public UnitOfWork()
        {
            var dbContextFactory = new DesignTimeDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(Array.Empty<string>());

            UserRepository = new UserRepository(DbContext);
        }

        public async Task CompleteAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await DbContext.DisposeAsync();
        }
    }
}