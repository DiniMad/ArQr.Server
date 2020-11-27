using System.Threading.Tasks;
using ArQr.Core.FileHandlers;
using AutoMapper;
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
        public async Task<ActionResult<UploadSessionResource>> CreateUploadSession(CreateUploadSessionResource sessionResource)
        {
            var (statusCode, value) = await _mediator.Send(new CreateUploadSessionRequest(sessionResource));
            return StatusCode(statusCode, value);
        }
    }
}