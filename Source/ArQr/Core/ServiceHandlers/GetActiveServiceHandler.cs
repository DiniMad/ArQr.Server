using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.ServiceHandlers
{
    public sealed record GetActiveServiceRequest(byte ServiceId) : IRequest<ActionHandlerResult>;

    public class GetActiveServiceHandler : IRequestHandler<GetActiveServiceRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork       _unitOfWork;
        private readonly IResponseMessages _responseMessages;
        private readonly IMapper           _mapper;

        public GetActiveServiceHandler(IUnitOfWork       unitOfWork,
                                       IResponseMessages responseMessages,
                                       IMapper           mapper)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _mapper           = mapper;
        }

        public async Task<ActionHandlerResult> Handle(GetActiveServiceRequest request,
                                                      CancellationToken       cancellationToken)
        {
            var serviceId = request.ServiceId;
            var service   = await _unitOfWork.ServiceRepository.GetAsync(serviceId);
            if (service is null || service.Active is false)
                return new(StatusCodes.Status404NotFound, _responseMessages.ServiceNotFound());

            return new(StatusCodes.Status200OK, _mapper.Map<ServiceResource>(service));
        }
    }
}