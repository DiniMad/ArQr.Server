using System.Threading.Tasks;
using ArQr.Core.SupportedMediaExtensionHandlers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArQr.Controllers
{
    [Route("SupportedMediaExtension")]
    [ApiController]
    public class SupportedMediaExtensionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupportedMediaExtensionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<SupportedMediaExtension>> GetAllSupportedMediaExtension()
        {
            var (statusCode, value) = await _mediator.Send(new GetAllSupportedMediaExtensionRequest());
            return StatusCode(statusCode, value);
        }
    }
}