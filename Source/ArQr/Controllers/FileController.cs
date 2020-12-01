using System.Threading.Tasks;
using ArQr.Core.FileHandlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("File")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("createSession")]
        public async Task<ActionResult> CreateUploadSession(
            CreateUploadSessionResource sessionResource)
        {
            var (statusCode, value) = await _mediator.Send(new CreateUploadSessionRequest(sessionResource));
            return StatusCode(statusCode, value);
        }

        [Authorize]
        [HttpPost("uploadChunk")]
        public async Task<ActionResult> UploadChunk(UploadChunkResource chunkResource)
        {
            var (statusCode, value) = await _mediator.Send(new UploadChunkRequest(chunkResource));
            return StatusCode(statusCode, value);
        }

        [Authorize]
        [HttpPost("uploadCompleted")]
        public async Task<ActionResult> UploadChunk(UploadCompletedResource completedResource)
        {
            var (statusCode, value) = await _mediator.Send(new UploadCompletedRequest(completedResource));
            return StatusCode(statusCode, value);
        }

        [HttpGet("download/{mediaContentId}")]
        public async Task<ActionResult> DownloadMedia(long mediaContentId)
        {
            var (statusCode, value) = await _mediator.Send(new DownloadMediaRequest(mediaContentId));
            return StatusCode(statusCode, value);
        }
    }
}