using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArQr.Infrastructure;

namespace ArQr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper              _mapper;

        public UserController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper    = mapper;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<UserResource>> GetUser(string id)
        {
            var contextUserId = HttpContext.GetUserId();
            if (id != null && contextUserId != id)
                return Unauthorized("You dont have permission to access the user detail.");

            var existingUser = await _dbContext.Users
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(user => user.Id == contextUserId);

            if (existingUser is null) return NotFound(id);
            return _mapper.Map<UserResource>(existingUser);
        }
    }
}