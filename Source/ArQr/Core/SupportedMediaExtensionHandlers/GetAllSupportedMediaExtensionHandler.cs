using System.Threading;
using System.Threading.Tasks;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArQr.Core.SupportedMediaExtensionHandlers
{
    public sealed record GetAllSupportedMediaExtensionRequest : IRequest<ActionHandlerResult>;


    public class GetAllSupportedMediaExtensionHandler :
        IRequestHandler<GetAllSupportedMediaExtensionRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSupportedMediaExtensionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionHandlerResult> Handle(GetAllSupportedMediaExtensionRequest request,
                                                      CancellationToken                    cancellationToken)
        {
            var supportedMediaExtensions =
                await _unitOfWork.SupportedMediaExtensionRepository.GetAllAsync();

            return new(StatusCodes.Status200OK, supportedMediaExtensions);
        }
    }
}