using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
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

    public sealed record GetSingleMyQrCodeRequest(long QrCodeId) : IRequest<ActionHandlerResult>;

    public class GetSingleUserQrCodeHandler : IRequestHandler<GetSingleUserQrCodeRequest, ActionHandlerResult>,
                                              IRequestHandler<GetSingleMyQrCodeRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IMapper                                _mapper;
        private readonly IHttpContextAccessor                   _httpContextAccessor;

        public GetSingleUserQrCodeHandler(IUnitOfWork                            unitOfWork,
                                          IStringLocalizer<HttpResponseMessages> responseMessages,
                                          IMapper                                mapper,
                                          IHttpContextAccessor                   httpContextAccessor)
        {
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionHandlerResult> Handle(GetSingleUserQrCodeRequest request,
                                                      CancellationToken          cancellationToken)
        {
            var qrCodeId = request.QrCodeId;
            return await Handle<QrCodeResource>(qrCodeId);
        }

        public async Task<ActionHandlerResult> Handle(GetSingleMyQrCodeRequest request,
                                                      CancellationToken        cancellationToken)
        {
            var qrCodeId = request.QrCodeId;
            var userId   = _httpContextAccessor.HttpContext!.GetUserId();
            return await Handle<AuthorizeQrCodeResource>(qrCodeId, userId);
        }

        private async Task<ActionHandlerResult> Handle<TResult>(long qrCodeId, long? userId = null)
            where TResult : QrCodeResource
        {
            var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);

            if (qrCode is null)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.QrCodeNotFound].Value);

            if (userId is not null && qrCode.OwnerId != userId)
                return new(StatusCodes.Status401Unauthorized,
                           _responseMessages[HttpResponseMessages.Unauthorized].Value);

            return new(StatusCodes.Status200OK, _mapper.Map<TResult>(qrCode));
        }
    }
}