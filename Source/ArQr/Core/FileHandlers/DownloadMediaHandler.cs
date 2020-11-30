using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArQr.Core.FileHandlers
{
    public sealed record DownloadMediaRequest(long MediaContentId) : IRequest<ActionHandlerResult>;

    public class DownloadMediaHandler : IRequestHandler<DownloadMediaRequest, ActionHandlerResult>
    {
        private readonly IFileStorage         _fileStorage;
        private readonly IResponseMessages    _responseMessages;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DownloadMediaHandler(IFileStorage         fileStorage,
                                    IResponseMessages    responseMessages,
                                    IUnitOfWork          unitOfWork,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _fileStorage         = fileStorage;
            _responseMessages    = responseMessages;
            _unitOfWork          = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionHandlerResult> Handle(DownloadMediaRequest request, CancellationToken cancellationToken)
        {
            var mediaContentId = request.MediaContentId;

            var mediaDirectoryExist = _fileStorage.DirectoryExist(mediaContentId.ToString());
            if (mediaDirectoryExist is false)
                return new(StatusCodes.Status404NotFound, _responseMessages.MediaContentNotFound());

            var mediaContent = await _unitOfWork.MediaContentRepository.GetAsync(mediaContentId);
            if (mediaContent is null)
                return new(StatusCodes.Status404NotFound, _responseMessages.MediaContentNotFound());

            var isMediaExpired = DateTimeOffset.UtcNow.ToUnixTimeSeconds() > mediaContent.ExpireDate;
            if (isMediaExpired is true) return new(StatusCodes.Status403Forbidden, _responseMessages.MediaExpired());

            if (mediaContent.Verified is false)
                return new(StatusCodes.Status403Forbidden, _responseMessages.MediaNotVerified());

            if (mediaContent.ExtensionId is null)
                return new(StatusCodes.Status404NotFound, _responseMessages.ExtensionNotSupported());
            
            var extension =
                await _unitOfWork.SupportedMediaExtensionRepository.GetAsync(mediaContent.ExtensionId.Value);
            if (extension is null) return new(StatusCodes.Status404NotFound, _responseMessages.ExtensionNotSupported());

            var directory = mediaContentId.ToString();

            var fileName = $"{mediaContent.Id}.{extension.Extension}";
            var fileSize = _fileStorage.CalculateChunksTotalSize(directory);

            if (fileSize == 0) return new(StatusCodes.Status404NotFound, _responseMessages.EmptyMedia());

            var response = _httpContextAccessor.HttpContext!.Response;
            response.ContentType                    = "application/octet-stream";
            response.ContentLength                  = fileSize;
            response.Headers["Content-Disposition"] = $"attachment; fileName={fileName}";

            await _fileStorage.WriteChunksFromDiskToStream(directory, response.Body);

            return new(StatusCodes.Status200OK, _responseMessages.Done());
        }
    }
}