using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Data.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Localization.ErrorKeys;
using Microsoft.Extensions.Localization;

namespace ArQr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork                _unitOfWork;
        private readonly IMapper                    _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public UserController(IUnitOfWork                unitOfWork,
                              IMapper                    mapper,
                              IStringLocalizer<Resource> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
            _localizer  = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = HttpContext.GetUserId();

            var existingUser = await _unitOfWork.Users.GetAsync(userId);

            return ApiResponse.Ok(_mapper.Map<UserResource>(existingUser));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var contextUserId = HttpContext.GetUserId();
            if (contextUserId != id) return ApiResponse.UnAuthorize(_localizer.GetUserError(UserErrors.UnAuthorize));

            var user = await _unitOfWork.Users.GetAsync(id);

            return ApiResponse.Ok(_mapper.Map<UserResource>(user));
        }
    }
}