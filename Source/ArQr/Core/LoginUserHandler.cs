using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Resource.Api.Resources;

namespace ArQr.Core
{
    public sealed record LoginUserRequest(UserLoginResource LoginResource) : IRequest<LoginUserResult>;

    public sealed record LoginUserResult(int StatusCode, object Value);

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResult>
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService         _tokenService;

        public LoginUserHandler(IUnitOfWork           unitOfWork,
                                IPasswordHasher<User> passwordHasher,
                                ITokenService         tokenService)
        {
            _unitOfWork     = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService   = tokenService;
        }

        public async Task<LoginUserResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loginResource = request.LoginResource;
            var user          = await _unitOfWork.UserRepository.GetIncludeRefreshTokenAsync(loginResource.PhoneNumber);
            if (user is null) return new(StatusCodes.Status404NotFound, "User Not Found.");

            try
            {
                var isPasswordValid = _passwordHasher.VerifyHashedPassword(user,
                                                                           user.PasswordHash,
                                                                           loginResource.Password);
                if (isPasswordValid == PasswordVerificationResult.Failed)
                    return new(StatusCodes.Status404NotFound, "Wrong Password.");
            }
            catch (FormatException)
            {
                return new(StatusCodes.Status404NotFound, "Wrong Password.");
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            if (user.RefreshToken is null)
                user.RefreshToken = newRefreshToken;
            else
            {
                user.RefreshToken.Token      = newRefreshToken.Token;
                user.RefreshToken.ExpireDate = newRefreshToken.ExpireDate;
            }

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            var jwtToken = _tokenService.GenerateJwtToken(user.GetClaims());
            return new(StatusCodes.Status200OK, jwtToken);
        }
    }
}