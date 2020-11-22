using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.QrCodeController
{
    public sealed record GetAllUserQrCodesRequest(long UserId) : IRequest<ActionHandlerResult>;

    public class GetAllUserQrCodesHandler : IRequestHandler<GetAllUserQrCodesRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public GetAllUserQrCodesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        public async Task<ActionHandlerResult> Handle(GetAllUserQrCodesRequest request,
                                                      CancellationToken        cancellationToken)
        {
            var userId = request.UserId;
            var userQrCodes =
                await _unitOfWork.QrCodeRepository.FindAsync(code => code.OwnerId == userId);

            if (userQrCodes.Any() is false) return new(StatusCodes.Status404NotFound, "NotFound");

            return new(StatusCodes.Status200OK, _mapper.Map<QrCodeResource>(userQrCodes));
        }
    }
}