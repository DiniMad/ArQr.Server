using System.Collections.Generic;
using System.Threading.Tasks;
using ArQr.Core.ServiceHandlers;
using ArQr.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{serviceId}")]
        public async Task<ActionResult<ServiceResource>> GetActiveService(byte serviceId)
        {
            var (statusCode, value) = await _mediator.Send(new GetActiveServiceRequest(serviceId));
            return StatusCode(statusCode, value);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<ServiceResource>> CreateService(CreateServiceResource serviceResource)
        {
            var (statusCode, value) = await _mediator.Send(new CreateServiceRequest(serviceResource));
            return statusCode == StatusCodes.Status201Created
                       ? CreatedAtAction("", "", null, value)
                       : StatusCode(statusCode, value);
        }
    }
}