using System;
using System.Threading.Tasks;
using Data.Repository.Base;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext               DbContext                         { get; }
        public  IUserRepository                    UserRepository                    { get; }
        public  IQrCodeRepository                  QrCodeRepository                  { get; }
        public  IQrCodeViewersRepository           QrCodeViewersRepository           { get; }
        public  IServiceRepository                 ServiceRepository                 { get; }
        public  IPurchaseRepository                PurchaseRepository                { get; }
        public  IMediaContentRepository            MediaContentRepository            { get; }
        public  ISupportedMediaExtensionRepository SupportedMediaExtensionRepository { get; }

        public UnitOfWork()
        {
            var dbContextFactory = new DesignTimeDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(Array.Empty<string>());

            UserRepository                    = new UserRepository(DbContext);
            QrCodeRepository                  = new QrCodeRepository(DbContext);
            QrCodeViewersRepository           = new QrCodeViewersRepository(DbContext);
            ServiceRepository                 = new ServiceRepository(DbContext);
            PurchaseRepository                = new PurchaseRepository(DbContext);
            MediaContentRepository            = new MediaContentRepository(DbContext);
            SupportedMediaExtensionRepository = new SupportedMediaExtensionRepository(DbContext);
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