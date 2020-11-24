using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ArQr.Core.QrCodeController
{
    public sealed record GetViewersCountRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetViewersCountHandler : IRequestHandler<GetViewersCountRequest, ActionHandlerResult>
    {
        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork   _unitOfWork;
        private readonly CacheOptions  _cacheOptions;

        public GetViewersCountHandler(ICacheService  cacheService,
                                      IUnitOfWork    unitOfWork,
                                      IConfiguration configuration)
        {
            _cacheService = cacheService;
            _unitOfWork   = unitOfWork;
            _cacheOptions = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(GetViewersCountRequest request,
                                                      CancellationToken      cancellationToken)
        {
            var qrCodeId = request.QrCodeId;
            var ghostKey = RedisSequenceKeyBuilder(_cacheOptions.GhostPrefix, _cacheOptions.QrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExist(ghostKey);
            if (ghostKeyExist is true)
            {
                var qrCodePersistedViewersCountKey =
                    RedisSequenceKeyBuilder(_cacheOptions.QrCodePrefix,
                                            _cacheOptions.PersistedViewersCountPrefix,
                                            qrCodeId);
                var persistedViewersCount = await _cacheService.GetAsync(qrCodePersistedViewersCountKey);
                if (persistedViewersCount is null) return new(StatusCodes.Status500InternalServerError, "Exception");

                var cachedViewerListKey =
                    RedisSequenceKeyBuilder(_cacheOptions.QrCodePrefix, _cacheOptions.ViewersListPrefix, qrCodeId);
                var cachedViewersCount = await _cacheService.GetCountOfListAsync(cachedViewerListKey);

                var totalCount = cachedViewersCount + long.Parse(persistedViewersCount);
                return new(StatusCodes.Status200OK, new {Count = totalCount});
            }
            else
            {
                var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
                if (qrCode is null) return new(StatusCodes.Status404NotFound, "NotFound");

                var totalCount = qrCode.ViewersCount;
                return new(StatusCodes.Status200OK, new {Count = totalCount});
            }
        }

        private string RedisSequenceKeyBuilder(params object[] keySections)
        {
            var key = string.Join(':', keySections);
            return key;
        }
    }
}