using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArQr.Infrastructure;
using ArQr.Models.Repositories;

namespace ArQr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper                    _mapper;

        public UserController(IApplicationUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper         = mapper;
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
            if (contextUserId != id) return ApiResponse.UnAuthorize("شما دسترسی لازم را ندارید.");

            var user = await _userRepository.GetUserAsync(id);

            if (user is {}) return ApiResponse.Ok(_mapper.Map<UserResource>(user));

            return ApiResponse.NotFound("کابر وجود ندارد.");
        }
    }
}