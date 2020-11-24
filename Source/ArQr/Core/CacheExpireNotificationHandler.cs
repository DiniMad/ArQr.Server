using System.Threading;
using System.Threading.Tasks;
using ArQr.Core.QrCodeHandlers;
using ArQr.Helper;
using ArQr.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ArQr.Core
{
    public sealed record CacheExpireNotification(string CacheKey) : INotification;

    public class CacheExpireNotificationHandler : INotificationHandler<CacheExpireNotification>
    {
        private readonly ISender      _sender;
        private readonly CacheOptions _cacheOptions;

        public CacheExpireNotificationHandler(IConfiguration configuration, ISender sender)
        {
            _sender       = sender;
            _cacheOptions = configuration.GetCacheOptions();
        }

        public async Task Handle(CacheExpireNotification notification, CancellationToken cancellationToken)
        {
            var key        = notification.CacheKey;
            var isGhostKey = _cacheOptions.IsGhostKey(key);
            if (isGhostKey is false) return;
            var rawKey   = _cacheOptions.ExtractRawKey(key);
            var isQrCode = _cacheOptions.KeyHasPrefix(rawKey, _cacheOptions.QrCodePrefix);
            if (isQrCode is true)
            {
                var qrCodeId = rawKey.Split(_cacheOptions.KyeSeparatorCharacter)[^1];
                await _sender.Send(new ViewersCacheExpireRequest(long.Parse(qrCodeId)), cancellationToken);
            }
        }
    }
}