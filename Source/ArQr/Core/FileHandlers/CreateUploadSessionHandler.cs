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
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.FileHandlers
{
    public sealed record CreateUploadSessionRequest(CreateUploadSessionResource SessionResource) :
        IRequest<ActionHandlerResult>;

    public class CreateUploadSessionHandler : IRequestHandler<CreateUploadSessionRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor                   _httpContextAccessor;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly ICacheService                          _cacheService;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IFileStorage                           _fileStorage;
        private readonly CacheOptions                           _cacheOptions;

        public CreateUploadSessionHandler(IHttpContextAccessor                   httpContextAccessor,
                                          IUnitOfWork                            unitOfWork,
                                          ICacheService                          cacheService,
                                          IConfiguration                         configuration,
                                          IStringLocalizer<HttpResponseMessages> responseMessages,
                                          IFileStorage                           fileStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _cacheService        = cacheService;
            _responseMessages    = responseMessages;
            _fileStorage         = fileStorage;
            _cacheOptions        = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(CreateUploadSessionRequest request,
                                                      CancellationToken          cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.GetUserId();

            var (mediaContentId, extensionName, totalSizeInMb) = request.SessionResource;

            var mediaContent = await _unitOfWork.MediaContentRepository.GetAsync(mediaContentId);
            if (mediaContent is null || mediaContent.UserId != userId)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.MediaContentNotFound].Value);

            if (totalSizeInMb > mediaContent.MaxSizeInMb)
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.ViolationOfMediaMaxSize].Value);


            var extension = await _unitOfWork.SupportedMediaExtensionRepository.GetAsync(extensionName);
            if (extension is null)
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.ExtensionNotSupported].Value);

            mediaContent.Verified    = false;
            mediaContent.ExtensionId = extension.Id;

            _unitOfWork.MediaContentRepository.Update(mediaContent);
            await _unitOfWork.CompleteAsync();

            var ghostPrefix                     = _cacheOptions.GhostPrefix;
            var mediaContentPrefix              = _cacheOptions.MediaContentPrefix;
            var uploadSessionPrefix             = _cacheOptions.UploadSessionPrefix;
            var uploadSessionExpireTimeInMinute = _cacheOptions.UploadSessionExpireTimeInMinute;

            var downloadMediaGhostKey =
                _cacheOptions.SequenceKeyBuilder(ghostPrefix, mediaContentPrefix, mediaContent.Id);
            await _cacheService.DeleteKeyAsync(downloadMediaGhostKey);

            var downloadMediaKey = _cacheOptions.SequenceKeyBuilder(mediaContentPrefix, mediaContent.Id);
            await _cacheService.DeleteKeyAsync(downloadMediaKey);

            var session = Guid.NewGuid();

            var uploadSessionGhostKey =
                _cacheOptions.SequenceKeyBuilder(ghostPrefix, uploadSessionPrefix, session);
            await _cacheService.SetAsync(uploadSessionGhostKey,
                                         string.Empty,
                                         TimeSpan.FromMinutes(uploadSessionExpireTimeInMinute));

            var uploadSessionKey =
                _cacheOptions.SequenceKeyBuilder(_cacheOptions.UploadSessionPrefix, session);
            CacheUploadSession uploadSession       = new(userId, mediaContent.MaxSizeInMb);
            var                uploadSessionString = JsonSerializer.Serialize(uploadSession);
            await _cacheService.SetAsync(uploadSessionKey, uploadSessionString);

            var directory    = userId.ToString();
            var subDirectory = mediaContentId.ToString();
            if (_fileStorage.DirectoryExist(directory, subDirectory))
                _fileStorage.DeleteDirectory(directory, subDirectory);

            return new(StatusCodes.Status200OK, new UploadSessionResource(session));
        }
    }
}