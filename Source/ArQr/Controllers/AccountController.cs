using System.Threading.Tasks;
using ArQr.Core.AccountHandlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResource>> Register(UserRegisterResource registerResource)
        {
            var (statusCode, value) = await _mediator.Send(new RegisterUserRequest(registerResource));
            return statusCode == StatusCodes.Status201Created
                       ? CreatedAtAction("GetMe", "User", null, value)
                       : StatusCode(statusCode, value);
        }

        [HttpPost("login")]
        public async Task<ActionResult<JwtTokenResource>> Login(UserLoginResource loginResource)
        {
            var (statusCode, value) = await _mediator.Send(new LoginUserRequest(loginResource));
            return StatusCode(statusCode, value);
        }

        [HttpPost("refresh_token")]
        public async Task<ActionResult<JwtTokenResource>> RefreshToken(RefreshTokenResource refreshTokenResource)
        {
            var (statusCode, value) = await _mediator.Send(new RefreshTokenRequest(refreshTokenResource));
            return StatusCode(statusCode, value);
        }
    }
}