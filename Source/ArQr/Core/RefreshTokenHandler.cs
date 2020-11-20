using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core
{
    public sealed record RefreshTokenRequest(RefreshTokenResource RefreshTokenResource) : IRequest<ActionHandlerResult>;

    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork   _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper       _mapper;

        public RefreshTokenHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            _unitOfWork   = unitOfWork;
            _tokenService = tokenService;
            _mapper       = mapper;
        }

        public async Task<ActionHandlerResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var refreshTokenResource = request.RefreshTokenResource;
            var user = await _unitOfWork.UserRepository.GetIncludeRefreshTokenAsync(refreshTokenResource.UserId);
            if (user is null) return new(StatusCodes.Status404NotFound, "User Not Found.");

            var isRefreshTokenValid = user.RefreshToken.IsExpired is false &&
                                      user.RefreshToken.Token == refreshTokenResource.RefreshToken;
            if (isRefreshTokenValid is false) return new(StatusCodes.Status401Unauthorized, "Unauthorized.");

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = _mapper.Map(newRefreshToken, user.RefreshToken);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            var jwtToken = _tokenService.GenerateJwtToken(user.GetClaims());
            return new(StatusCodes.Status200OK, jwtToken);
        }
    }
}