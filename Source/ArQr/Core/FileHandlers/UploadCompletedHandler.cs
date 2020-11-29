using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.FileHandlers
{
    public sealed record UploadCompletedRequest(UploadCompletedResource UploadCompletedResource) :
        IRequest<ActionHandlerResult>;

    public class UploadCompletedHandler : IRequestHandler<UploadCompletedRequest, ActionHandlerResult>
    {
        private readonly ICacheService                          _cacheService;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IHttpContextAccessor                   _httpContextAccessor;
        private readonly CacheOptions                           _cacheOptions;

        public UploadCompletedHandler(ICacheService                          cacheService,
                                      IConfiguration                         configuration,
                                      IStringLocalizer<HttpResponseMessages> responseMessages,
                                      IHttpContextAccessor                   httpContextAccessor)
        {
            _cacheService        = cacheService;
            _responseMessages    = responseMessages;
            _httpContextAccessor = httpContextAccessor;
            _cacheOptions        = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(UploadCompletedRequest request,
                                                      CancellationToken      cancellationToken)
        {
            var session = request.UploadCompletedResource.Session;

            var ghostPrefix         = _cacheOptions.GhostPrefix;
            var uploadSessionPrefix = _cacheOptions.UploadSessionPrefix;
            var chunkListPrefix     = _cacheOptions.ChunkListPrefix;

            var uploadSessionGhostKey =
                _cacheOptions.SequenceKeyBuilder(ghostPrefix, uploadSessionPrefix, session);
            var sessionExist = await _cacheService.KeyExistAsync(uploadSessionGhostKey);
            if (sessionExist is false)
                return new(StatusCodes.Status410Gone,
                           _responseMessages[HttpResponseMessages.SessionExpired].Value);

            var uploadSessionKey   = _cacheOptions.SequenceKeyBuilder(uploadSessionPrefix, session);
            var cacheSessionString = await _cacheService.GetAsync(uploadSessionKey);
            if (cacheSessionString is null)
                return new(StatusCodes.Status500InternalServerError,
                           _responseMessages[HttpResponseMessages.UnhandledException].Value);

            var userId       = _httpContextAccessor.HttpContext!.GetUserId();
            var cacheSession = JsonSerializer.Deserialize<CacheUploadSession>(cacheSessionString);
            if (cacheSession!.UserId != userId)
                return new(StatusCodes.Status401Unauthorized,
                           _responseMessages[HttpResponseMessages.Unauthorized].Value);

            var uploadedChunksListKey = _cacheOptions.SequenceKeyBuilder(chunkListPrefix, session);

            await _cacheService.DeleteKeyAsync(uploadSessionGhostKey);
            await _cacheService.DeleteKeyAsync(uploadSessionKey);
            await _cacheService.DeleteKeyAsync(uploadedChunksListKey);

            return new(StatusCodes.Status200OK, _responseMessages[HttpResponseMessages.Done].Value);
        }
    }
}