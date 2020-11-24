using System.Collections.Generic;
using System.Linq;
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
    public sealed record GetAllUserQrCodesRequest
        (long UserId, PaginationInputResource PaginationInputResource) : IRequest<ActionHandlerResult>;

    public class GetAllUserQrCodesHandler : IRequestHandler<GetAllUserQrCodesRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IMapper                                _mapper;
        private readonly IHttpContextAccessor                   _httpContextAccessor;

        public GetAllUserQrCodesHandler(IUnitOfWork                            unitOfWork,
                                        IStringLocalizer<HttpResponseMessages> responseMessages,
                                        IMapper                                mapper,
                                        IHttpContextAccessor                   httpContextAccessor)
        {
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionHandlerResult> Handle(GetAllUserQrCodesRequest request,
                                                      CancellationToken        cancellationToken)
        {
            var userId          = request.UserId;
            var paginationInput = request.PaginationInputResource;
            var userQrCodes =
                await _unitOfWork.QrCodeRepository.FindAsync(code => code.OwnerId == userId,
                                                             paginationInput.After,
                                                             paginationInput.PageSize);

            if (userQrCodes.Any() is false)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.QrCodeNotFound].Value);

            var userQrCodesResource = _mapper.Map<IEnumerable<QrCodeResource>>(userQrCodes);
            var userQrCodeCount = await _unitOfWork.QrCodeRepository.GetCountAsync(code => code.OwnerId == userId);
            var baseUrl         = _httpContextAccessor.HttpContext!.GetFullUrl();
            
            PaginationResultResource<QrCodeResource> paginationResult =
                new(userQrCodesResource, userQrCodeCount, baseUrl, paginationInput);
            
            return new(StatusCodes.Status200OK, paginationResult);
        }
    }
}