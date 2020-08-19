using System.Linq;
using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper                      _mapper;
        private readonly IStringLocalizer<Resource>   _localizer;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 IMapper                      mapper,
                                 IStringLocalizer<Resource>   localizer)
        {
            _userManager = userManager;
            _mapper      = mapper;
            _localizer   = localizer;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserResource model)
        {
            var user   = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiResponse.BadRequest(_localizer.GetUserManagerCreateError(result.Errors.First().Code));

            var location = Url.Action("GetUser", "User", new {id = user.Id});
            return ApiResponse.Created(location, _mapper.Map<UserResource>(user));
        }
    }
}