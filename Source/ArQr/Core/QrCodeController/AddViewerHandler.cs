using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Resource.Api.Resources;

namespace ArQr.Core.QrCodeController
{
    public sealed record AddViewerRequest
        (long QrCodeId, AddViewerResource ViewerResource) : IRequest<ActionHandlerResult>;

    public class AddViewerHandler : IRequestHandler<AddViewerRequest, ActionHandlerResult>
    {
        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork   _unitOfWork;
        private readonly CacheOptions  _cacheOptions;

        public AddViewerHandler(ICacheService  cacheService,
                                IUnitOfWork    unitOfWork,
                                IConfiguration configuration)
        {
            _cacheService = cacheService;
            _unitOfWork   = unitOfWork;
            _cacheOptions = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(AddViewerRequest request, CancellationToken cancellationToken)
        {
            var qrCodeId = request.QrCodeId;
            var ghostKey =
                _cacheOptions.SequenceKeyBuilder(_cacheOptions.GhostPrefix, _cacheOptions.QrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExist(ghostKey);
            if (ghostKeyExist is false)
            {
                var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
                if (qrCode is null) return new(StatusCodes.Status404NotFound, "QrCode not found.");

                var qrCodePersistedViewersCountValue = qrCode.ViewersCount.ToString();
                var qrCodePersistedViewersCountKey =
                    _cacheOptions.SequenceKeyBuilder(_cacheOptions.QrCodePrefix,
                                                     _cacheOptions.PersistedViewersCountPrefix,
                                                     qrCodeId);
                await _cacheService.SetAsync(qrCodePersistedViewersCountKey, qrCodePersistedViewersCountValue);
            }

            await _cacheService.SetAsync(ghostKey,
                                         string.Empty,
                                         TimeSpan.FromMinutes(_cacheOptions.ViewersCountExpireTimeInMinute));

            var viewerListKey =
                _cacheOptions.SequenceKeyBuilder(_cacheOptions.QrCodePrefix, _cacheOptions.ViewersListPrefix, qrCodeId);
            var viewerIdValue = request.ViewerResource.ViewerId.ToString();
            await _cacheService.AddToUniqueListAsync(viewerListKey, viewerIdValue);

            return new(StatusCodes.Status200OK, "Viewer Added.");
        }
    }
}