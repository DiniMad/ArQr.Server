using System;
using System.Text.Json;
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

namespace ArQr.Core.FileHandlers
{
    public sealed record CreateUploadSessionRequest(CreateUploadSessionResource SessionResource) :
        IRequest<ActionHandlerResult>;

    public class CreateUploadSessionHandler : IRequestHandler<CreateUploadSessionRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly ICacheService        _cacheService;
        private readonly IResponseMessages    _responseMessages;
        private readonly IFileStorage         _fileStorage;
        private readonly CacheOptions         _cacheOptions;
        private readonly FileChunksOptions    _fileChunksOptions;

        public CreateUploadSessionHandler(IHttpContextAccessor httpContextAccessor,
                                          IUnitOfWork          unitOfWork,
                                          ICacheService        cacheService,
                                          IConfiguration       configuration,
                                          IResponseMessages    responseMessages,
                                          IFileStorage         fileStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _cacheService        = cacheService;
            _responseMessages    = responseMessages;
            _fileStorage         = fileStorage;
            _cacheOptions        = configuration.GetCacheOptions();
            _fileChunksOptions   = configuration.GetFileChunksOptions();
        }

        public async Task<ActionHandlerResult> Handle(CreateUploadSessionRequest request,
                                                      CancellationToken          cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.GetUserId();

            var (mediaContentId, extensionName, totalSizeInMb) = request.SessionResource;

            var mediaContent = await _unitOfWork.MediaContentRepository.GetAsync(mediaContentId);
            if (mediaContent is null || mediaContent.UserId != userId)
                return new(StatusCodes.Status404NotFound, _responseMessages.MediaContentNotFound());

            if (totalSizeInMb > mediaContent.MaxSizeInMb)
                return new(StatusCodes.Status400BadRequest, _responseMessages.ViolationOfMediaMaxSize());

            var extension = await _unitOfWork.SupportedMediaExtensionRepository.GetAsync(extensionName);
            if (extension is null)
                return new(StatusCodes.Status400BadRequest, _responseMessages.ExtensionNotSupported());

            mediaContent.Verified    = false;
            mediaContent.ExtensionId = extension.Id;

            _unitOfWork.MediaContentRepository.Update(mediaContent);
            await _unitOfWork.CompleteAsync();

            var ghostPrefix                     = _cacheOptions.GhostPrefix;
            var uploadSessionPrefix             = _cacheOptions.UploadSessionPrefix;
            var uploadSessionExpireTimeInMinute = _cacheOptions.UploadSessionExpireTimeInMinute;

            var uploadSessionGhostKey =
                _cacheOptions.SequenceKeyBuilder(ghostPrefix, uploadSessionPrefix, mediaContentId);
            await _cacheService.SetAsync(uploadSessionGhostKey,
                                         string.Empty,
                                         TimeSpan.FromMinutes(uploadSessionExpireTimeInMinute));

            var uploadSessionKey =
                _cacheOptions.SequenceKeyBuilder(_cacheOptions.UploadSessionPrefix, mediaContentId);
            CacheUploadSession uploadSession       = new(userId, mediaContent.MaxSizeInMb, extensionName);
            var                uploadSessionString = JsonSerializer.Serialize(uploadSession);
            await _cacheService.SetAsync(uploadSessionKey, uploadSessionString);

            var directory = mediaContentId.ToString();
            _fileStorage.ReCreateDirectory(directory);

            return new(StatusCodes.Status200OK,
                       new UploadSessionResource(_fileChunksOptions.UploadChunkSize));
        }
    }
}