using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ArQr.Core.FileHandlers
{
    public sealed record UploadSessionExpiredRequest(string session) : IRequest<Unit>;

    public class UploadSessionExpiredHandler : IRequestHandler<UploadSessionExpiredRequest, Unit>
    {
        private readonly IFileStorage  _fileStorage;
        private readonly ICacheService _cacheService;
        private readonly CacheOptions  _cacheOptions;

        public UploadSessionExpiredHandler(IFileStorage   fileStorage,
                                           ICacheService  cacheService,
                                           IConfiguration configuration)
        {
            _fileStorage  = fileStorage;
            _cacheService = cacheService;
            _cacheOptions = configuration.GetCacheOptions();
        }

        public async Task<Unit> Handle(UploadSessionExpiredRequest request, CancellationToken cancellationToken)
        {
            var session = request.session;

            var uploadSessionPrefix = _cacheOptions.UploadSessionPrefix;
            var chunkListPrefix     = _cacheOptions.ChunkListPrefix;

            var uploadSessionKey   = _cacheOptions.SequenceKeyBuilder(uploadSessionPrefix, session);
            var cacheSessionString = await _cacheService.GetAsync(uploadSessionKey);
            if (cacheSessionString is null) return Unit.Value;

            var cacheSession = JsonSerializer.Deserialize<CacheUploadSession>(cacheSessionString);
            if (cacheSession is null) return Unit.Value;

            var directory             = cacheSession.MediaContentId.ToString();
            var sessionDirectoryExist = _fileStorage.DirectoryExist(directory);
            if (sessionDirectoryExist is true) _fileStorage.DeleteDirectory(directory);

            var uploadedChunksListKey = _cacheOptions.SequenceKeyBuilder(chunkListPrefix, session);

            await _cacheService.DeleteKeyAsync(uploadSessionKey);
            await _cacheService.DeleteKeyAsync(uploadedChunksListKey);

            return Unit.Value;
        }
    }
}