using System.Threading.Tasks;
using ArQr.Core.ServiceHandlers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArQr.Controllers
{
    [Route("Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<Service>> GetAllActiveServices()
        {
            var (statusCode, value) = await _mediator.Send(new GetAllActiveServicesRequest());
            return StatusCode(statusCode, value);
        }
    }
}