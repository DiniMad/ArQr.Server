using System;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
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
        private readonly ITokenService         _tokenService;
        private readonly IMapper               _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(IUnitOfWork           unitOfWork,
                                 ITokenService         tokenService,
                                 IMapper               mapper,
                                 IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork     = unitOfWork;
            _tokenService   = tokenService;
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginResource loginResource)
        {
            var searchResult =
                await _unitOfWork.UserRepository
                                 .FindAsync(u => u.PhoneNumber == loginResource.PhoneNumber);
            var user = searchResult.ToList().FirstOrDefault();
            if (user is null) return NotFound();

            try
            {
                var isPasswordValid = _passwordHasher.VerifyHashedPassword(user,
                                                                           user.PasswordHash,
                                                                           loginResource.Password);
                if (isPasswordValid == PasswordVerificationResult.Failed) return BadRequest("Wrong Password");
            }
            catch (FormatException)
            {
                return BadRequest("Wrong Password");
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            var jwtToken = _tokenService.GenerateJwtToken(user.GetClaims());
            return Ok(jwtToken);
        }
    }
}