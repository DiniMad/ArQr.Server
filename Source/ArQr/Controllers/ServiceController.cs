using System.Collections.Generic;
using System.Threading.Tasks;
using ArQr.Core.ServiceHandlers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

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

        public async Task<ActionResult<IEnumerable<ServiceResource>>> GetAllActiveServices()
        {
            var (statusCode, value) = await _mediator.Send(new GetAllActiveServicesRequest());
            return StatusCode(statusCode, value);
        }
    }
}