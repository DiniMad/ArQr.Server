using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record UpdateMyQrCodeRequest(long QrCodeId, UpdateQrCodeResource QrCodeResource) :
        IRequest<ActionHandlerResult>;

    public class UpdateMyQrCodeHandler : IRequestHandler<UpdateMyQrCodeRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IResponseMessages    _responseMessages;
        private readonly IMapper              _mapper;

        public UpdateMyQrCodeHandler(IHttpContextAccessor httpContextAccessor,
                                     IUnitOfWork          unitOfWork,
                                     IResponseMessages    responseMessages,
                                     IMapper              mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
        }

        public async Task<ActionHandlerResult> Handle(UpdateMyQrCodeRequest request,
                                                      CancellationToken     cancellationToken)
        {
            var qrCodeId = request.QrCodeId;
            var qrCode   = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
            if (qrCode is null) return new(StatusCodes.Status404NotFound, _responseMessages.QrCodeNotFound());

            var userId = _httpContextAccessor.HttpContext!.GetUserId();
            if (qrCode.OwnerId != userId) return new(StatusCodes.Status404NotFound, _responseMessages.QrCodeNotFound());

            var newQrCode = _mapper.Map(request.QrCodeResource,qrCode);
            _unitOfWork.QrCodeRepository.Update(newQrCode);
            await _unitOfWork.CompleteAsync();

            return new(StatusCodes.Status200OK, _responseMessages.Done());
        }
    }
}