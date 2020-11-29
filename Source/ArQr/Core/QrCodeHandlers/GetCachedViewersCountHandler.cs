using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Resource.Api.Resources;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record GetCachedViewersCountRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetCachedViewersCountHandler : IRequestHandler<GetCachedViewersCountRequest, ActionHandlerResult>
    {
        private readonly ICacheService     _cacheService;
        private readonly IResponseMessages _responseMessages;
        private readonly CacheOptions      _cacheOptions;

        public GetCachedViewersCountHandler(ICacheService     cacheService,
                                            IConfiguration    configuration,
                                            IResponseMessages responseMessages)
        {
            _cacheService     = cacheService;
            _responseMessages = responseMessages;
            _cacheOptions     = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(GetCachedViewersCountRequest request,
                                                      CancellationToken            cancellationToken)
        {
            var ghostPrefix       = _cacheOptions.GhostPrefix;
            var qrCodePrefix      = _cacheOptions.QrCodePrefix;
            var viewersListPrefix = _cacheOptions.ViewersListPrefix;

            var qrCodeId      = request.QrCodeId;
            var ghostKey      = _cacheOptions.SequenceKeyBuilder(ghostPrefix, qrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExistAsync(ghostKey);
            if (ghostKeyExist is false) return new(StatusCodes.Status404NotFound, _responseMessages.QrCodeNotFound());

            var cachedViewerListKey =
                _cacheOptions.SequenceKeyBuilder(qrCodePrefix, viewersListPrefix, qrCodeId);
            var cachedViewersCount = await _cacheService.GetCountOfListAsync(cachedViewerListKey);

            return new(StatusCodes.Status200OK,
                       new QrCodeViewersCountResource((int) cachedViewersCount));
        }
    }
}