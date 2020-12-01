using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.MediaContentHandlers
{
    public sealed record VerifyMediaContentRequest(VerifyMediaContentResource VerifyMediaContentResource) :
        IRequest<ActionHandlerResult>;

    public class VerifyMediaContentHandler : IRequestHandler<VerifyMediaContentRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork       _unitOfWork;
        private readonly IResponseMessages _responseMessages;

        public VerifyMediaContentHandler(IUnitOfWork unitOfWork, IResponseMessages responseMessages)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
        }

        public async Task<ActionHandlerResult> Handle(VerifyMediaContentRequest request,
                                                      CancellationToken         cancellationToken)
        {
            var mediaContentId = request.VerifyMediaContentResource.MediaContentId;
            var mediaContent   = await _unitOfWork.MediaContentRepository.GetAsync(mediaContentId);
            if (mediaContent is null)
                return new(StatusCodes.Status404NotFound, _responseMessages.MediaContentNotFound());

            mediaContent.Verified = request.VerifyMediaContentResource.Verify;
            _unitOfWork.MediaContentRepository.Update(mediaContent);
            await _unitOfWork.CompleteAsync();

            return new(StatusCodes.Status200OK, _responseMessages.Done());
        }
    }
}