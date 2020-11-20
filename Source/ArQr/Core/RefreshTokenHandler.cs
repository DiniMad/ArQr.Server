using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core
{
    public sealed record RefreshTokenRequest(RefreshTokenResource RefreshTokenResource) : IRequest<RefreshTokenResult>;

    public sealed record RefreshTokenResult(int StatusCode, object Value);

    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResult>
    {
        private readonly IUnitOfWork   _unitOfWork;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork   = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var refreshTokenResource = request.RefreshTokenResource;
            var user = await _unitOfWork.UserRepository.GetIncludeRefreshTokenAsync(refreshTokenResource.UserId);
            if (user is null) return new(StatusCodes.Status404NotFound, "User Not Found.");

            var isRefreshTokenValid = user.RefreshToken.IsExpired is false &&
                                      user.RefreshToken.Token == refreshTokenResource.RefreshToken;
            if (isRefreshTokenValid is false) return new(StatusCodes.Status401Unauthorized, "Unauthorized.");

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken.Token      = newRefreshToken.Token;
            user.RefreshToken.ExpireDate = newRefreshToken.ExpireDate;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            var jwtToken = _tokenService.GenerateJwtToken(user.GetClaims());
            return new(StatusCodes.Status200OK, jwtToken);
        }
    }
}