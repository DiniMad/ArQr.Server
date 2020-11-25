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
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record AddViewerRequest
        (long QrCodeId, AddViewerResource ViewerResource) : IRequest<ActionHandlerResult>;

    public class AddViewerHandler : IRequestHandler<AddViewerRequest, ActionHandlerResult>
    {
        private readonly ICacheService                          _cacheService;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly CacheOptions                           _cacheOptions;

        public AddViewerHandler(ICacheService                          cacheService,
                                IUnitOfWork                            unitOfWork,
                                IConfiguration                         configuration,
                                IStringLocalizer<HttpResponseMessages> responseMessages)
        {
            _cacheService     = cacheService;
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _cacheOptions     = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(AddViewerRequest request, CancellationToken cancellationToken)
        {
            var (ghostPrefix, qrCodePrefix, persistedViewersCountPrefix, viewersListPrefix, expireTimeInMinute) =
                _cacheOptions;

            var qrCodeId      = request.QrCodeId;
            var ghostKey      = _cacheOptions.SequenceKeyBuilder(ghostPrefix, qrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExistAsync(ghostKey);
            if (ghostKeyExist is false)
            {
                var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
                if (qrCode is null)
                    return new(StatusCodes.Status404NotFound,
                               _responseMessages[HttpResponseMessages.QrCodeNotFound].Value);

                var qrCodePersistedViewersCountValue = qrCode.ViewersCount.ToString();
                var qrCodePersistedViewersCountKey =
                    _cacheOptions.SequenceKeyBuilder(qrCodePrefix, persistedViewersCountPrefix, qrCodeId);
                await _cacheService.SetAsync(qrCodePersistedViewersCountKey, qrCodePersistedViewersCountValue);
            }

            await _cacheService.SetAsync(ghostKey, string.Empty, TimeSpan.FromMinutes(expireTimeInMinute));

            var viewerListKey = _cacheOptions.SequenceKeyBuilder(qrCodePrefix, viewersListPrefix, qrCodeId);
            var viewerIdValue = request.ViewerResource.ViewerId.ToString();
            await _cacheService.AddToUniqueListAsync(viewerListKey, viewerIdValue);

            return new(StatusCodes.Status200OK, _responseMessages[HttpResponseMessages.Done].Value);
        }
    }
}