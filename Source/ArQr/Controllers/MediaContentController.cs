using System.Threading.Tasks;
using ArQr.Core.MediaContentHandlers;
using ArQr.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("MediaContent")]
    public class MediaContentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediaContentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> VerifyMediaContent(VerifyMediaContentResource verifyMediaContentResource)
        {
            var (statusCode, value) = await _mediator.Send(new VerifyMediaContentRequest(verifyMediaContentResource));
            return StatusCode(statusCode, value);
        }
    }
}