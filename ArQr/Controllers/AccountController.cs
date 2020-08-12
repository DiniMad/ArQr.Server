using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper                      _mapper;
        private readonly LinkGenerator                _linkGenerator;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, LinkGenerator linkGenerator)
        {
            _userManager   = userManager;
            _mapper        = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResource>> RegisterUser(RegisterUserResource model)
        {
            var user   = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            var location = _linkGenerator.GetPathByAction("GetUser",
                                                          "User",
                                                          new {id = user.Id});
            return Created(location, _mapper.Map<UserResource>(user));
        }
    }
}