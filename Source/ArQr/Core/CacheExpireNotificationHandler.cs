using System.Threading;
using System.Threading.Tasks;
using ArQr.Core.FileHandlers;
using ArQr.Core.QrCodeHandlers;
using ArQr.Helper;
using ArQr.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArQr.Core
{
    public sealed record CacheExpireNotification(string CacheKey) : INotification;

    public class CacheExpireNotificationHandler : INotificationHandler<CacheExpireNotification>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CacheOptions         _cacheOptions;

        public CacheExpireNotificationHandler(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _cacheOptions = configuration.GetCacheOptions();
        }

        public async Task Handle(CacheExpireNotification notification, CancellationToken cancellationToken)
        {
            var key        = notification.CacheKey;
            var isGhostKey = _cacheOptions.IsGhostKey(key);
            if (isGhostKey is false) return;
            var rawKey = _cacheOptions.ExtractRawKey(key);

            var isQrCode = _cacheOptions.KeyHasPrefix(rawKey, _cacheOptions.QrCodePrefix);
            if (isQrCode is true)
            {
                var qrCodeId = rawKey.Split(_cacheOptions.KyeSeparatorCharacter)[^1];

                using var serviceScope = _scopeFactory.CreateScope();
                var       sender       = serviceScope.ServiceProvider.GetRequiredService<ISender>();
                await sender.Send(new ViewersCacheExpireRequest(long.Parse(qrCodeId)), cancellationToken);
            }

            var isUploadSession = _cacheOptions.KeyHasPrefix(rawKey, _cacheOptions.UploadSessionPrefix);
            if (isUploadSession is true)
            {
                var session = rawKey.Split(_cacheOptions.KyeSeparatorCharacter)[^1];

                using var serviceScope = _scopeFactory.CreateScope();
                var       sender       = serviceScope.ServiceProvider.GetRequiredService<ISender>();
                await sender.Send(new UploadSessionExpiredRequest(session), cancellationToken);
            }
        }
    }
}