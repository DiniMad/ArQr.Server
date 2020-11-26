using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.ServiceHandlers
{
    public sealed record GetAllActiveServicesRequest : IRequest<ActionHandlerResult>;

    public class GetAllActiveServicesHandler : IRequestHandler<GetAllActiveServicesRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public GetAllActiveServicesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        public async Task<ActionHandlerResult> Handle(GetAllActiveServicesRequest request,
                                                      CancellationToken           cancellationToken)
        {
            var activeServices =
                await _unitOfWork.ServiceRepository.FindAsync(service => service.Active);
            return new(StatusCodes.Status200OK,
                       _mapper.Map<IEnumerable<ServiceResource>>(activeServices));
        }
    }
}