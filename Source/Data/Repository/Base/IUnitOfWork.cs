using System;
using System.Threading.Tasks;
using Domain.Base;

namespace Data.Repository.Base
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository                    UserRepository                    { get; }
        public IQrCodeRepository                  QrCodeRepository                  { get; }
        public IQrCodeViewersRepository           QrCodeViewersRepository           { get; }
        public IServiceRepository                 ServiceRepository                 { get; }
        public IPurchaseRepository                PurchaseRepository                { get; }
        public IMediaContentRepository            MediaContentRepository            { get; }
        public ISupportedMediaExtensionRepository SupportedMediaExtensionRepository { get; }

        public Task InsertAsync<TKey>(BaseDomain<TKey> domain) where TKey : struct;
        public Task CompleteAsync();
    }
}