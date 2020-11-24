using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record GetSingleUserQrCodeRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetSingleUserQrCodeHandler : IRequestHandler<GetSingleUserQrCodeRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IMapper                                _mapper;

        public GetSingleUserQrCodeHandler(IUnitOfWork                            unitOfWork,
                                          IStringLocalizer<HttpResponseMessages> responseMessages,
                                          IMapper                                mapper)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _mapper           = mapper;
        }

        public async Task<ActionHandlerResult> Handle(GetSingleUserQrCodeRequest request,
                                                      CancellationToken          cancellationToken)
        {
            var qrCodeId = request.QrCodeId;
            var qrCode   = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);

            if (qrCode is null)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.QrCodeNotFound].Value);

            return new(StatusCodes.Status200OK, _mapper.Map<QrCodeResource>(qrCode));
        }
    }
}