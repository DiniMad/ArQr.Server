using System.Threading;
using System.Threading.Tasks;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArQr.Core.ServiceHandlers
{
    public sealed record GetAllActiveServicesRequest : IRequest<ActionHandlerResult>;

    public class GetAllActiveServicesHandler : IRequestHandler<GetAllActiveServicesRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllActiveServicesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionHandlerResult> Handle(GetAllActiveServicesRequest request,
                                                      CancellationToken           cancellationToken)
        {
            var activeServices =
                await _unitOfWork.ServiceRepository.FindAsync(service => service.Active);
            return new(StatusCodes.Status200OK, activeServices);
        }
    }
}