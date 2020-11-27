using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Infrastructure;
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
        private readonly CacheOptions                           _cacheOptions;

        public CreateUploadSessionHandler(IHttpContextAccessor                   httpContextAccessor,
                                          IUnitOfWork                            unitOfWork,
                                          ICacheService                          cacheService,
                                          IConfiguration                         configuration,
                                          IStringLocalizer<HttpResponseMessages> responseMessages)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _cacheService        = cacheService;
            _responseMessages    = responseMessages;
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
                           _responseMessages[HttpResponseMessages.MediaContentNotFound]);

            var extension = await _unitOfWork.SupportedMediaExtensionRepository.GetAsync(extensionName);
            if (extension is null)
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.ExtensionNotSupported]);

            mediaContent.Verified      = false;
            mediaContent.TotalSizeInMb = totalSizeInMb;
            mediaContent.ExtensionId   = extension.Id;

            _unitOfWork.MediaContentRepository.Update(mediaContent);
            await _unitOfWork.CompleteAsync();

            var downloadMediaGhostKey = _cacheOptions.SequenceKeyBuilder("gh", "mc", mediaContent.Id);
            await _cacheService.DeleteKeyAsync(downloadMediaGhostKey);

            var downloadMediaKey = _cacheOptions.SequenceKeyBuilder("mc", mediaContent.Id);
            await _cacheService.DeleteKeyAsync(downloadMediaKey);

            var session = Guid.NewGuid();

            var uploadSessionGhostKey = _cacheOptions.SequenceKeyBuilder("gh", "us", session);
            await _cacheService.SetAsync(uploadSessionGhostKey, string.Empty, TimeSpan.FromSeconds(30));

            var                uploadSessionKey    = _cacheOptions.SequenceKeyBuilder("us", session);
            CacheUploadSession uploadSession       = new(userId, totalSizeInMb);
            var                uploadSessionString = JsonSerializer.Serialize(uploadSession);
            await _cacheService.SetAsync(uploadSessionKey, uploadSessionString);

            return new(StatusCodes.Status200OK, new UploadSessionResource(session));
        }
    }
}