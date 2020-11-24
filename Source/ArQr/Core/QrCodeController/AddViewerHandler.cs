using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.QrCodeController
{
    public sealed record AddViewerRequest
        (long QrCodeId, AddViewerResource ViewerResource) : IRequest<ActionHandlerResult>;

    public class AddViewerHandler : IRequestHandler<AddViewerRequest, ActionHandlerResult>
    {
        private const    string        GhostPrefix                 = "gh";
        private const    string        QrCodePrefix                = "qc";
        private const    string        PersistedViewersCountPrefix = "pvc";
        private const    string        ViewersListPrefix           = "vl";
        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork   _unitOfWork;

        public AddViewerHandler(ICacheService cacheService, IUnitOfWork unitOfWork)
        {
            _cacheService = cacheService;
            _unitOfWork   = unitOfWork;
        }

        public async Task<ActionHandlerResult> Handle(AddViewerRequest request, CancellationToken cancellationToken)
        {
            var qrCodeId      = request.QrCodeId;
            var ghostKey      = RedisSequenceKeyBuilder(GhostPrefix, QrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.GetAsync(ghostKey) is not null;
            if (ghostKeyExist is false)
            {
                var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
                if (qrCode is null) return new(StatusCodes.Status404NotFound, "QrCode not found.");

                var qrCodePersistedViewersCountValue = qrCode.ViewersCount.ToString();
                var qrCodePersistedViewersCountKey =
                    RedisSequenceKeyBuilder(QrCodePrefix, PersistedViewersCountPrefix, qrCodeId);
                await _cacheService.SetAsync(qrCodePersistedViewersCountKey, qrCodePersistedViewersCountValue);

                await _cacheService.SetAsync(ghostKey, string.Empty, TimeSpan.FromMinutes(10));
            }

            var viewerListKey = RedisSequenceKeyBuilder(QrCodePrefix, ViewersListPrefix, qrCodeId);
            var viewerIdValue = request.ViewerResource.ViewerId.ToString();
            await _cacheService.AddToUniqueListAsync(viewerListKey, viewerIdValue);
            return new(StatusCodes.Status200OK, "Viewer Added.");
        }

        private string RedisSequenceKeyBuilder(params object[] keySections)
        {
            var key = string.Join(':', keySections);
            return key;
        }
    }
}