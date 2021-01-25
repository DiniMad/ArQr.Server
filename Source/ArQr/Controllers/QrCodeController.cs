using System.Collections.Generic;
using System.Threading.Tasks;
using ArQr.Core.QrCodeHandlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("/user/{userId}/qrCode")]
        public async Task<ActionResult<IEnumerable<QrCodeResource>>> GetAllUserQrCodes(long userId,
            [FromQuery] PaginationInputResource                                             paginationInputResource)
        {
            var (statusCode, value) =
                await _mediator.Send(new GetAllUserQrCodesRequest(userId, paginationInputResource));
            return StatusCode(statusCode, value);
        }

        [Authorize]
        [HttpGet("/user/me/qrCode")]
        public async Task<ActionResult<IEnumerable<AuthorizeQrCodeResource>>> GetAllMyQrCodes(
            [FromQuery] PaginationInputResource paginationInputResource)
        {
            var (statusCode, value) =
                await _mediator.Send(new GetAllMyQrCodesRequest(paginationInputResource));
            return StatusCode(statusCode, value);
        }

        [HttpGet("{qrCodeId}")]
        public async Task<ActionResult<IEnumerable<QrCodeResource>>> GetSingleUserQrCode(long qrCodeId)
        {
            var (statusCode, value) = await _mediator.Send(new GetSingleQrCodeRequest(qrCodeId));
            return StatusCode(statusCode, value);
        }

        [Authorize]
        [HttpPost("/user/me/qrCode/{qrCodeId}")]
        public async Task<ActionResult<QrCodeResource>> UpdateMyQrCode(long                 qrCodeId,
                                                                       UpdateQrCodeResource qrCodeResource)
        {
            var (statusCode, value) = await _mediator.Send(new UpdateMyQrCodeRequest(qrCodeId, qrCodeResource));
            return StatusCode(statusCode, value);
        }

        [HttpPost("{qrCodeId}")]
        public async Task<ActionResult> AddViewer(long qrCodeId, AddViewerResource viewerResource)
        {
            var (statusCode, value) = await _mediator.Send(new AddViewerRequest(qrCodeId, viewerResource));
            return StatusCode(statusCode, value);
        }

        [HttpGet("{qrCodeId}/cachedViewersCount")]
        public async Task<ActionResult> GetCachedViewersCount(long qrCodeId)
        {
            var (statusCode, value) = await _mediator.Send(new GetCachedViewersCountRequest(qrCodeId));
            return StatusCode(statusCode, value);
        }
    }
}