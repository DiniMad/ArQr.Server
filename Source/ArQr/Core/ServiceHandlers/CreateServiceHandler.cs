using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.ServiceHandlers
{
    public sealed record CreateServiceRequest(CreateServiceResource ServiceResource) : IRequest<ActionHandlerResult>;

    public class CreateServiceHandler : IRequestHandler<CreateServiceRequest, ActionHandlerResult>
    {
        private readonly IMapper     _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionHandlerResult> Handle(CreateServiceRequest request, CancellationToken cancellationToken)
        {
            var serviceResource = request.ServiceResource;
            var service         = _mapper.Map<Service>(serviceResource);
            await _unitOfWork.ServiceRepository.InsertAsync(service);
            await _unitOfWork.CompleteAsync();

            return new(StatusCodes.Status201Created, _mapper.Map<ServiceResource>(service));
        }
    }
}