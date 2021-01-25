using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record GetSingleQrCodeRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetSingleUserQrCodeHandler : IRequestHandler<GetSingleQrCodeRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IResponseMessages    _responseMessages;
        private readonly IMapper              _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetSingleUserQrCodeHandler(IUnitOfWork          unitOfWork,
                                          IResponseMessages    responseMessages,
                                          IMapper              mapper,
                                          IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionHandlerResult> Handle(GetSingleQrCodeRequest request,
                                                      CancellationToken      cancellationToken)
        {
            var authenticated = _httpContextAccessor.HttpContext!.Authenticated();
            var qrCodeId      = request.QrCodeId;

            if (authenticated is false) return await Handle(qrCodeId);

            var userId = _httpContextAccessor.HttpContext!.GetUserId();
            return await Handle(qrCodeId, userId);
        }

        private async Task<ActionHandlerResult> Handle(long qrCodeId, long? userId = null)
        {
            var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);

            if (qrCode is null) return new(StatusCodes.Status404NotFound, _responseMessages.QrCodeNotFound());

            if (userId is null || qrCode.OwnerId != userId)
                return new(StatusCodes.Status200OK, _mapper.Map<QrCodeResource>(qrCode));

            return new(StatusCodes.Status200OK, _mapper.Map<AuthorizeQrCodeResource>(qrCode));
        }
    }
}