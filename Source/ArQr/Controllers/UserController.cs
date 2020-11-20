using System.Threading.Tasks;
using ArQr.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArQr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        public async Task<ActionResult<User>> GetMe()
        {
            var (statusCode, result) = await _mediator.Send(new UserGetMeRequest());
            return StatusCode(statusCode, result);
        }
    }
}