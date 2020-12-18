using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blazor.ApiResources;
using Blazor.Helpers;
using Blazor.Models;
using Blazored.LocalStorage;
using MediatR;

namespace Blazor.Handlers
{
    public sealed record SilentSigninRequest : IRequest<ApiResponse<JwtTokenResource>>;

    public class SilentSigninHandler : IRequestHandler<SilentSigninRequest, ApiResponse<JwtTokenResource>>
    {
        private readonly ISyncLocalStorageService _localStorage;
        private readonly HttpClient               _httpClient;
        private readonly Endpoints                _endpoints;

        public SilentSigninHandler(ISyncLocalStorageService localStorage, HttpClient httpClient, Endpoints endpoints)
        {
            _localStorage = localStorage;
            _httpClient   = httpClient;
            _endpoints    = endpoints;
        }

        public async Task<ApiResponse<JwtTokenResource>> Handle(SilentSigninRequest request,
                                                                CancellationToken   cancellationToken)
        {
            var refreshToken         = _localStorage.GetItem<string>(LocalStorageKeys.RefreshToken);
            var userId               = _localStorage.GetItem<long>(LocalStorageKeys.UserId);
            var refreshTokenResource = new RefreshTokenResource(userId, refreshToken);
            var response =
                await _httpClient.PostAsync<JwtTokenResource>(_endpoints.Server.RefreshToken, refreshTokenResource);
            return response;
        }
    }
}