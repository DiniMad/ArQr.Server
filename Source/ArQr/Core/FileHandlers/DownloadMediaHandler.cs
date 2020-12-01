using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ArQr.Core.FileHandlers
{
    public sealed record DownloadMediaRequest(long MediaContentId) : IRequest<ActionHandlerResult>;

    public class DownloadMediaHandler : IRequestHandler<DownloadMediaRequest, ActionHandlerResult>
    {
        private readonly IFileStorage         _fileStorage;
        private readonly IResponseMessages    _responseMessages;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FileChunksOptions    _fileChunksOptions;

        public DownloadMediaHandler(IFileStorage         fileStorage,
                                    IResponseMessages    responseMessages,
                                    IUnitOfWork          unitOfWork,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfiguration       configuration)
        {
            _fileStorage         = fileStorage;
            _responseMessages    = responseMessages;
            _unitOfWork          = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _fileChunksOptions   = configuration.GetFileChunksOptions();
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
            var fileName  = $"{mediaContentId}.{extension.Extension}";
            var fileSize  = _fileStorage.GetFileSize(directory, fileName);

            if (fileSize == 0) return new(StatusCodes.Status404NotFound, _responseMessages.EmptyMedia());

            var response = _httpContextAccessor.HttpContext!.Response;
            response.ContentType                    = "application/octet-stream";
            response.ContentLength                  = fileSize;
            response.Headers["Content-Disposition"] = $"attachment; fileName={fileName}";

            await _fileStorage.WriteFromDiskToStream(directory,
                                                     fileName,
                                                     response.Body,
                                                     _fileChunksOptions.DownloadChunkSize);

            return new(StatusCodes.Status200OK, _responseMessages.Done());
        }
    }
}