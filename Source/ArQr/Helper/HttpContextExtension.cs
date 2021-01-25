using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArQr.Helper
{
    public static class HttpContextExtension
    {
        public static bool Authenticated(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return userId is not null;
        }

        public static long GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return long.Parse(userId);
        }

        public static string GetFullUrl(this HttpContext httpContext)
        {
            var httpRequest = httpContext.Request;
            var baseUrl     = $"{httpRequest.Scheme}://{httpRequest.Host.Value}{httpRequest.Path.Value!.TrimEnd('/')}/";
            return baseUrl;
        }
    }
}