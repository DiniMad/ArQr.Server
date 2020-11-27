using System;
using System.IO;
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
    public sealed record UploadChunkRequest(UploadChunkResource ChunkResource) : IRequest<ActionHandlerResult>;

    public class UploadChunkHandler : IRequestHandler<UploadChunkRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor                   _httpContextAccessor;
        private readonly ICacheService                          _cacheService;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly CacheOptions                           _cacheOptions;

        public UploadChunkHandler(IHttpContextAccessor                   httpContextAccessor,
                                  ICacheService                          cacheService,
                                  IConfiguration                         configuration,
                                  IStringLocalizer<HttpResponseMessages> responseMessages)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheService        = cacheService;
            _responseMessages    = responseMessages;
            _cacheOptions        = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(UploadChunkRequest request, CancellationToken cancellationToken)
        {
            var ghostPrefix                     = _cacheOptions.GhostPrefix;
            var uploadSessionPrefix             = _cacheOptions.UploadSessionPrefix;
            var uploadSessionExpireTimeInMinute = _cacheOptions.UploadSessionExpireTimeInMinute;
            
            var session = request.ChunkResource.Session;
            var uploadSessionGhostKey =
                _cacheOptions.SequenceKeyBuilder(ghostPrefix, uploadSessionPrefix, session);
            var sessionExist = await _cacheService.KeyExistAsync(uploadSessionGhostKey);
            if (sessionExist is false) return new(StatusCodes.Status410Gone, "SessionExpired");

            var uploadSessionKey =
                _cacheOptions.SequenceKeyBuilder(_cacheOptions.UploadSessionPrefix, session);
            var cacheSessionString = await _cacheService.GetAsync(uploadSessionKey);
            if (cacheSessionString is null)
                return new(StatusCodes.Status500InternalServerError,
                           _responseMessages[HttpResponseMessages.UnhandledException].Value);

            var userId = _httpContextAccessor.HttpContext!.GetUserId();
            var cacheSession = JsonSerializer.Deserialize<CacheUploadSession>(cacheSessionString);
            if (cacheSession!.UserId != userId)
                return new(StatusCodes.Status401Unauthorized,
                           _responseMessages[HttpResponseMessages.Unauthorized].Value);

            var uploadedChunksListKey = _cacheOptions.SequenceKeyBuilder("cl", session);
            var chunkNumber           = request.ChunkResource.ChunkNumber.ToString();
            await _cacheService.AddToUniqueListAsync(uploadedChunksListKey, chunkNumber);

            var uploadedChunksCount = await _cacheService.GetCountOfListAsync(uploadedChunksListKey);
            var uploadedSizeInMb    = uploadedChunksCount;


            var uploadedSizeAllowed = uploadedSizeInMb <= cacheSession.MaxSizeInMb;
            if (uploadedSizeAllowed is false)
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.ViolationOfMediaMaxSize].Value);

            await _cacheService.SetAsync(uploadSessionGhostKey,
                                         string.Empty,
                                         TimeSpan.FromMinutes(uploadSessionExpireTimeInMinute));

            var             buffer = request.ChunkResource.Content;
            await using var file   = File.OpenWrite(chunkNumber);
            await file.WriteAsync(buffer, cancellationToken);

            return new(StatusCodes.Status200OK, _responseMessages[HttpResponseMessages.Done].Value);
        }
    }
}