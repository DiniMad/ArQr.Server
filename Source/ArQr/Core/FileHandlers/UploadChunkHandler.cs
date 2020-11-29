using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Resource.Api.Resources;

namespace ArQr.Core.FileHandlers
{
    public sealed record UploadChunkRequest(UploadChunkResource ChunkResource) : IRequest<ActionHandlerResult>;

    public class UploadChunkHandler : IRequestHandler<UploadChunkRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService        _cacheService;
        private readonly IResponseMessages    _responseMessages;
        private readonly IFileStorage         _fileStorage;
        private readonly CacheOptions         _cacheOptions;

        public UploadChunkHandler(IHttpContextAccessor httpContextAccessor,
                                  ICacheService        cacheService,
                                  IConfiguration       configuration,
                                  IResponseMessages    responseMessages,
                                  IFileStorage         fileStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheService        = cacheService;
            _responseMessages    = responseMessages;
            _fileStorage         = fileStorage;
            _cacheOptions        = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(UploadChunkRequest request, CancellationToken cancellationToken)
        {
            var ghostPrefix                     = _cacheOptions.GhostPrefix;
            var uploadSessionPrefix             = _cacheOptions.UploadSessionPrefix;
            var chunkListPrefix                 = _cacheOptions.ChunkListPrefix;
            var uploadSessionExpireTimeInMinute = _cacheOptions.UploadSessionExpireTimeInMinute;

            var session = request.ChunkResource.Session;
            var uploadSessionGhostKey =
                _cacheOptions.SequenceKeyBuilder(ghostPrefix, uploadSessionPrefix, session);
            var sessionExist = await _cacheService.KeyExistAsync(uploadSessionGhostKey);
            if (sessionExist is false) return new(StatusCodes.Status410Gone, _responseMessages.SessionExpired());


            var uploadSessionKey =
                _cacheOptions.SequenceKeyBuilder(_cacheOptions.UploadSessionPrefix, session);
            var cacheSessionString = await _cacheService.GetAsync(uploadSessionKey);
            if (cacheSessionString is null)
                return new(StatusCodes.Status500InternalServerError,
                           _responseMessages.UnhandledException());

            var userId       = _httpContextAccessor.HttpContext!.GetUserId();
            var cacheSession = JsonSerializer.Deserialize<CacheUploadSession>(cacheSessionString);
            if (cacheSession!.UserId != userId)
                return new(StatusCodes.Status401Unauthorized, _responseMessages.Unauthorized());

            var uploadedChunksListKey = _cacheOptions.SequenceKeyBuilder(chunkListPrefix, session);
            var chunkNumber           = request.ChunkResource.ChunkNumber.ToString();
            await _cacheService.AddToUniqueListAsync(uploadedChunksListKey, chunkNumber);

            var uploadedChunksCount = await _cacheService.GetCountOfListAsync(uploadedChunksListKey);
            var uploadedSizeInMb    = uploadedChunksCount;


            var uploadedSizeAllowed = uploadedSizeInMb <= cacheSession.MaxSizeInMb;
            if (uploadedSizeAllowed is false)
                return new(StatusCodes.Status400BadRequest, _responseMessages.ViolationOfMediaMaxSize());

            await _cacheService.SetAsync(uploadSessionGhostKey,
                                         string.Empty,
                                         TimeSpan.FromMinutes(uploadSessionExpireTimeInMinute));

            var directory = cacheSession.MediaContentId.ToString();
            var fileName  = chunkNumber;
            var content   = request.ChunkResource.Content;
            await _fileStorage.WriteFileAsync(directory, fileName, content);

            return new(StatusCodes.Status200OK, _responseMessages.Done());
        }
    }
}