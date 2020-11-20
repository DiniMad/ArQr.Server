using System.Threading.Tasks;
using ArQr.Helper;
using AutoMapper;
using Data.Repository.Base;
using Domain;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        [HttpGet("Me")]
        public async Task<ActionResult<User>> GetMe()
        {
            var userId = HttpContext.GetUserId();
            var user   = await _unitOfWork.UserRepository.GetAsync(userId);
            if (user is null) return NotFound();
            return Ok(_mapper.Map<UserResource>(user));
        }
    }
}