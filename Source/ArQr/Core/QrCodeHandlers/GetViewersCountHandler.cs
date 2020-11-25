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
    public sealed record GetViewersCountRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetViewersCountHandler : IRequestHandler<GetViewersCountRequest, ActionHandlerResult>
    {
        private readonly ICacheService                          _cacheService;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly CacheOptions                           _cacheOptions;

        public GetViewersCountHandler(ICacheService                          cacheService,
                                      IUnitOfWork                            unitOfWork,
                                      IConfiguration                         configuration,
                                      IStringLocalizer<HttpResponseMessages> responseMessages)
        {
            _cacheService     = cacheService;
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _cacheOptions     = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(GetViewersCountRequest request,
                                                      CancellationToken      cancellationToken)
        {
            var (ghostPrefix, qrCodePrefix, persistedViewersCountPrefix, viewersListPrefix, _) = _cacheOptions;

            var qrCodeId      = request.QrCodeId;
            var ghostKey      = _cacheOptions.SequenceKeyBuilder(ghostPrefix, qrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExistAsync(ghostKey);
            if (ghostKeyExist is true)
            {
                var qrCodePersistedViewersCountKey =
                    _cacheOptions.SequenceKeyBuilder(qrCodePrefix, persistedViewersCountPrefix, qrCodeId);
                var persistedViewersCount = await _cacheService.GetAsync(qrCodePersistedViewersCountKey);
                if (persistedViewersCount is null)
                    return new(StatusCodes.Status500InternalServerError,
                               _responseMessages[HttpResponseMessages.UnhandledException].Value);

                var cachedViewerListKey =
                    _cacheOptions.SequenceKeyBuilder(qrCodePrefix, viewersListPrefix, qrCodeId);
                var cachedViewersCount = await _cacheService.GetCountOfListAsync(cachedViewerListKey);

                var totalCount = cachedViewersCount + long.Parse(persistedViewersCount);
                return new(StatusCodes.Status200OK, new QrCodeViewersCountResource((int) totalCount));
            }
            else
            {
                var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
                if (qrCode is null)
                    return new(StatusCodes.Status404NotFound,
                               _responseMessages[HttpResponseMessages.QrCodeNotFound].Value);

                var totalCount = qrCode.ViewersCount;
                return new(StatusCodes.Status200OK, new QrCodeViewersCountResource(totalCount));
            }
        }
    }
}