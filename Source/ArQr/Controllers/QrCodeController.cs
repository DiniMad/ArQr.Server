using System.Collections.Generic;
using System.Threading.Tasks;
using ArQr.Core.QrCodeController;
using ArQr.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("QrCode")]
    public class QrCodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QrCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/User/{userId}/QrCode")]
        public async Task<ActionResult<IEnumerable<QrCodeResource>>> GetAllUserQrCodes(long userId,
            [FromQuery] PaginationInputResource                                             paginationInputResource)
        {
            var (statusCode, value) =
                await _mediator.Send(new GetAllUserQrCodesRequest(userId, paginationInputResource));
            return StatusCode(statusCode, value);
        }

        [HttpGet("{qrCodeId}")]
        public async Task<ActionResult<IEnumerable<QrCodeResource>>> GetSingleUserQrCode(long qrCodeId)
        {
            var (statusCode, value) = await _mediator.Send(new GetSingleUserQrCodeRequest(qrCodeId));
            return StatusCode(statusCode, value);
        }

        [HttpPost("{qrCodeId}")]
        public async Task<ActionResult> AddViewer(long qrCodeId, AddViewerResource viewerResource)
        {
            var (statusCode, value) = await _mediator.Send(new AddViewerRequest(qrCodeId, viewerResource));
            return StatusCode(statusCode, value);
        }
    }
}