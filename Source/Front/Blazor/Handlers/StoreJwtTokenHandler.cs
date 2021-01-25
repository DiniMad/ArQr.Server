using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Models;
using Blazor.States.ApiToken;
using Blazored.LocalStorage;
using MediatR;

namespace Blazor.Handlers
{
    public sealed record StoreJwtTokenRequest(string Token) : IRequest<Unit>;

    public class StoreJwtTokenHandler : IRequestHandler<StoreJwtTokenRequest, Unit>
    {
        private readonly ISender                  _sender;
        private readonly ISyncLocalStorageService _localStorage;

        public StoreJwtTokenHandler(ISender sender, ISyncLocalStorageService localStorage)
        {
            _sender       = sender;
            _localStorage = localStorage;
        }

        public async Task<Unit> Handle(StoreJwtTokenRequest request, CancellationToken cancellationToken)
        {
            var token  = request.Token;
            var claims = GetClaims(token).ToArray();

            var refreshToken = claims.First(claim => claim.Type == ClaimKeys.RefreshToken).Value;
            _localStorage.SetItem(LocalStorageKeys.RefreshToken, refreshToken);

            var idClaim              = claims.First(claim => claim.Type == ClaimKeys.Id).Value;
            _localStorage.SetItem(LocalStorageKeys.UserId, idClaim);
            
            var expClaim             = claims.First(claim => claim.Type == ClaimKeys.Expire).Value;
            var expireDateInUnixTime = long.Parse(expClaim);
            var expireDate           = DateTimeOffset.FromUnixTimeSeconds(expireDateInUnixTime).LocalDateTime;
            await _sender.Send(new AuthenticationState.SetTokenAction {Value = token, ExpireDate = expireDate});

            return Unit.Value;
        }


        private IEnumerable<Claim> GetClaims(string token)
        {
            var tokenHandler  = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return securityToken!.Claims;
        }
    }
}