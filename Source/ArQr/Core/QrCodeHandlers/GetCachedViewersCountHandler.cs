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
    public sealed record GetCachedViewersCountRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetCachedViewersCountHandler : IRequestHandler<GetCachedViewersCountRequest, ActionHandlerResult>
    {
        private readonly ICacheService                          _cacheService;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly CacheOptions                           _cacheOptions;

        public GetCachedViewersCountHandler(ICacheService                          cacheService,
                                            IConfiguration                         configuration,
                                            IStringLocalizer<HttpResponseMessages> responseMessages)
        {
            _cacheService     = cacheService;
            _responseMessages = responseMessages;
            _cacheOptions     = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(GetCachedViewersCountRequest request,
                                                      CancellationToken            cancellationToken)
        {
            var (ghostPrefix, qrCodePrefix, _, viewersListPrefix, _) = _cacheOptions;

            var qrCodeId      = request.QrCodeId;
            var ghostKey      = _cacheOptions.SequenceKeyBuilder(ghostPrefix, qrCodePrefix, qrCodeId);
            var ghostKeyExist = await _cacheService.KeyExistAsync(ghostKey);
            if (ghostKeyExist is false)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.QrCodeNotFound].Value);

            var cachedViewerListKey =
                _cacheOptions.SequenceKeyBuilder(qrCodePrefix, viewersListPrefix, qrCodeId);
            var cachedViewersCount = await _cacheService.GetCountOfListAsync(cachedViewerListKey);

            return new(StatusCodes.Status200OK,
                       new QrCodeViewersCountResource((int) cachedViewersCount));
        }
    }
}