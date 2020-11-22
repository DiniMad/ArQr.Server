using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.QrCodeController
{
    public sealed record GetAllUserQrCodesRequest
        (long UserId, PaginationInputResource PaginationInputResource) : IRequest<ActionHandlerResult>;

    public class GetAllUserQrCodesHandler : IRequestHandler<GetAllUserQrCodesRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IMapper                                _mapper;

        public GetAllUserQrCodesHandler(IUnitOfWork                            unitOfWork,
                                        IStringLocalizer<HttpResponseMessages> responseMessages,
                                        IMapper                                mapper)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _mapper           = mapper;
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

            return new(StatusCodes.Status200OK, _mapper.Map<IEnumerable<QrCodeResource>>(userQrCodes));
        }
    }
}