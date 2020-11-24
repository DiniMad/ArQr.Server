using System;
using System.Threading.Tasks;
using Data.Repository.Base;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext     DbContext               { get; }
        public  IUserRepository          UserRepository          { get; }
        public  IQrCodeRepository        QrCodeRepository        { get; }
        public  IQrCodeViewersRepository QrCodeViewersRepository { get; }

        public UnitOfWork()
        {
            var dbContextFactory = new DesignTimeDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(Array.Empty<string>());

            UserRepository          = new UserRepository(DbContext);
            QrCodeRepository        = new QrCodeRepository(DbContext);
            QrCodeViewersRepository = new QrCodeViewersRepository(DbContext);
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