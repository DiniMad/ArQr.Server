using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Localization.ErrorKeys;
using ArQr.Models.Repositories;
using Microsoft.Extensions.Localization;

namespace ArQr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper                    _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public UserController(IApplicationUserRepository userRepository,
                              IMapper                    mapper,
                              IStringLocalizer<Resource> localizer)
        {
            _userRepository = userRepository;
            _mapper         = mapper;
            _localizer      = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = HttpContext.GetUserId();

            var existingUser = await _userRepository.GetUserAsync(userId);

            return ApiResponse.Ok(_mapper.Map<UserResource>(existingUser));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var contextUserId = HttpContext.GetUserId();
            if (contextUserId != id) return ApiResponse.UnAuthorize(_localizer.GetUserError(UserErrors.UnAuthorize));

            var user = await _userRepository.GetUserAsync(id);

            return ApiResponse.Ok(_mapper.Map<UserResource>(user));
        }
    }
}