using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly IMapper               _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(IUnitOfWork           unitOfWork,
                                 IMapper               mapper,
                                 IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork     = unitOfWork;
            _mapper         = mapper;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterResource registerResource)
        {
            var user = _mapper.Map<User>(registerResource);
            user.PasswordHash = _passwordHasher.HashPassword(user, registerResource.Password);

            await _unitOfWork.UserRepository.InsertAsync(user);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return (e.InnerException as SqlException)?.Number == 2601
                           ? BadRequest("Phone Number already taken.")
                           : Problem("Unhandled Exception.");
            }

            return Created("", user);
        }
    }
}