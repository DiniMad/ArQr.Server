using System.Collections.Generic;
using System.Threading.Tasks;
using ArQr.Core.QrCodeController;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("user/{userId}/QrCode")]
    public class QrCodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QrCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QrCodeResource>>> GetAllUserQrCodes(long userId)
        {
            var (statusCode, value) = await _mediator.Send(new GetAllUserQrCodesRequest(userId));
            return StatusCode(statusCode, value);
        }
    }
}