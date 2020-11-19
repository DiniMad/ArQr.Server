using System;
using System.Threading.Tasks;
using ArQr.Core;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly ITokenService         _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMediator             _mediator;

        public AccountController(IUnitOfWork           unitOfWork,
                                 ITokenService         tokenService,
                                 IPasswordHasher<User> passwordHasher,
                                 IMediator             mediator)
        {
            _unitOfWork     = unitOfWork;
            _tokenService   = tokenService;
            _passwordHasher = passwordHasher;
            _mediator       = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterResource registerResource)
        {
            var (statusCode, value) = await _mediator.Send(new RegisterUserRequest(registerResource));
            return statusCode == StatusCodes.Status201Created ? Created("", value) : StatusCode(statusCode, value);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginResource loginResource)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(loginResource.PhoneNumber);
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