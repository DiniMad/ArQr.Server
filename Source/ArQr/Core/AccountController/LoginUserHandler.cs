using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.AccountController
{
    public sealed record LoginUserRequest(UserLoginResource LoginResource) : IRequest<ActionHandlerResult>;

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IPasswordHasher<User>                  _passwordHasher;
        private readonly ITokenService                          _tokenService;
        private readonly IMapper                                _mapper;

        public LoginUserHandler(IUnitOfWork                            unitOfWork,
                                IStringLocalizer<HttpResponseMessages> responseMessages,
                                IPasswordHasher<User>                  passwordHasher,
                                ITokenService                          tokenService,
                                IMapper                                mapper)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _passwordHasher   = passwordHasher;
            _tokenService     = tokenService;
            _mapper           = mapper;
        }

        public async Task<ActionHandlerResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loginResource = request.LoginResource;
            var user          = await _unitOfWork.UserRepository.GetIncludeRefreshTokenAsync(loginResource.PhoneNumber);
            if (user is null)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.UserNotFound].Value);

            try
            {
                var isPasswordValid = _passwordHasher.VerifyHashedPassword(user,
                                                                           user.PasswordHash,
                                                                           loginResource.Password);
                if (isPasswordValid == PasswordVerificationResult.Failed)
                    return new(StatusCodes.Status400BadRequest,
                               _responseMessages[HttpResponseMessages.IncorrectPassword].Value);
            }
            catch (FormatException)
            {
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.IncorrectPassword].Value);
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = user.RefreshToken is null
                                    ? newRefreshToken
                                    : _mapper.Map(newRefreshToken, user.RefreshToken);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            var jwtToken = _tokenService.GenerateJwtToken(user.GetClaims());
            return new(StatusCodes.Status200OK, new JwtTokenResource(jwtToken));
        }
    }
}