using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper                      _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper      = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResource>> RegisterUser(RegisterUserResource model)
        {
            var user   = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) return Created("", _mapper.Map<UserResource>(user));
            return BadRequest(result.Errors);
        }
    }
}