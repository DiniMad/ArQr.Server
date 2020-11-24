using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArQr.Core.QrCodeController
{
    public sealed record GetViewersCountRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetViewersCountHandler : IRequestHandler<GetViewersCountRequest, ActionHandlerResult>
    {
        private const string GhostPrefix                 = "gh";
        private const string QrCodePrefix                = "qc";
        private const string PersistedViewersCountPrefix = "pvc";
        private const string ViewersListPrefix           = "vl";

        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork   _unitOfWork;

        public GetViewersCountHandler(ICacheService cacheService, IUnitOfWork unitOfWork)
        {
            _cacheService = cacheService;
            _unitOfWork   = unitOfWork;
        }

        public async Task<ActionHandlerResult> Handle(GetViewersCountRequest request,
                                                      CancellationToken      cancellationToken)
        {
            var qrCodeId      = request.QrCodeId;
            var ghostKey      = RedisSequenceKeyBuilder(GhostPrefix, QrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExist(ghostKey);
            if (ghostKeyExist is true)
            {
                var qrCodePersistedViewersCountKey =
                    RedisSequenceKeyBuilder(QrCodePrefix, PersistedViewersCountPrefix, qrCodeId);
                var persistedViewersCount = await _cacheService.GetAsync(qrCodePersistedViewersCountKey);
                if (persistedViewersCount is null) return new(StatusCodes.Status500InternalServerError, "Exception");

                var cachedViewerListKey = RedisSequenceKeyBuilder(QrCodePrefix, ViewersListPrefix, qrCodeId);
                var cachedViewersCount  = await _cacheService.GetCountOfListAsync(cachedViewerListKey);

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