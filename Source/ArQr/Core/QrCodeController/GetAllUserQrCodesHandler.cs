using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArQr.Core.QrCodeController
{
    public sealed record GetAllUserQrCodesRequest(long UserId) : IRequest<ActionHandlerResult>;

    public class GetAllUserQrCodesHandler : IRequestHandler<GetAllUserQrCodesRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUserQrCodesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionHandlerResult> Handle(GetAllUserQrCodesRequest request,
                                                      CancellationToken        cancellationToken)
        {
            var userId      = request.UserId;
            var userQrCodes = 
                await _unitOfWork.QrCodeRepository.FindAsync(code => code.OwnerId == userId);
            
            if (userQrCodes.Any() is false) return new(StatusCodes.Status404NotFound, "NotFound");
            
            return new(StatusCodes.Status200OK, userQrCodes);
        }
    }
}