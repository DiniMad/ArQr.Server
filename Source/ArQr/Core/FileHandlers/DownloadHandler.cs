using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Resource.ResourceFiles;

namespace ArQr.Core.FileHandlers
{
    public sealed record DownloadRequest(long MediaContentId) : IRequest<ActionHandlerResult>;

    public class DownloadHandler : IRequestHandler<DownloadRequest, ActionHandlerResult>
    {
        private readonly IFileStorage                           _fileStorage;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IHttpContextAccessor                   _httpContextAccessor;

        public DownloadHandler(IFileStorage                           fileStorage,
                               IStringLocalizer<HttpResponseMessages> responseMessages,
                               IUnitOfWork                            unitOfWork,
                               IHttpContextAccessor                   httpContextAccessor)
        {
            _fileStorage         = fileStorage;
            _responseMessages    = responseMessages;
            _unitOfWork          = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionHandlerResult> Handle(DownloadRequest request, CancellationToken cancellationToken)
        {
            var mediaContentId = request.MediaContentId;

            var mediaDirectoryExist = _fileStorage.DirectoryExist(mediaContentId.ToString());
            if (mediaDirectoryExist is false)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.MediaContentNotFound].Value);

            var mediaContent = await _unitOfWork.MediaContentRepository.GetAsync(mediaContentId);
            if (mediaContent is null)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.MediaContentNotFound].Value);

            var extension = await _unitOfWork.SupportedMediaExtensionRepository.GetAsync(mediaContent.ExtensionId);
            if (extension is null)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.ExtensionNotSupported].Value);
            
            var directory = mediaContentId.ToString();
            
            var fileName = $"{mediaContent.Id}.{extension.Extension}";
            var fileSize = _fileStorage.CalculateChunksTotalSize(directory);
            
            var response = _httpContextAccessor.HttpContext!.Response;
            response.ContentType                    = "application/octet-stream";
            response.ContentLength                  = fileSize;
            response.Headers["Content-Disposition"] = $"attachment; fileName={fileName}";

            await _fileStorage.WriteChunksFromDiskToStream(directory, response.Body);

            return new(StatusCodes.Status200OK, _responseMessages[HttpResponseMessages.Done].Value);
        }
    }
}