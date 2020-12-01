using System.Threading.Tasks;
using ArQr.Core.SupportedMediaExtensionHandlers;
using ArQr.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<SupportedMediaExtension>> CreateMediaExtension(
            CreateSupportedMediaExtensionResource extensionResource)
        {
            var (statusCode, value) = await _mediator.Send(new CreateMediaExtensionRequest(extensionResource));
            return StatusCode(statusCode, value);
        }
    }
}