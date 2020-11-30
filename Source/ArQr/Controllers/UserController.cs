using System.Threading.Tasks;
using ArQr.Core.UserHandlers;
using ArQr.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

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
        public async Task<ActionResult<UserResource>> GetMe()
        {
            var (statusCode, value) = await _mediator.Send(new UserGetMeRequest());
            return StatusCode(statusCode, value);
        }

        [HttpPost("update")]
        public async Task<ActionResult<UserResource>> UpdateMe(UserUpdateResource updateResource)
        {
            var (statusCode, value) = await _mediator.Send(new UserUpdateMeRequest(updateResource));
            return StatusCode(statusCode, value);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("admin")]
        public async Task<ActionResult> MakeUserAdmin(MakeUserAdminResource adminResource)
        {
            var (statusCode, value) = await _mediator.Send(new MakeUserAdminRequest(adminResource));
            return StatusCode(statusCode, value);
        }
    }
}