using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArQr.Helper
{
    public static class HttpContextExtension
    {
        public sealed record UserAuthentication(bool IsAuthenticated, Guid Id);

        public static Guid GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return new Guid(userId);
        }

        public static UserAuthentication GetUserAuthentication(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            var isAuthenticated = userId is not null;
            var userGuidId      = isAuthenticated ? Guid.Parse(userId!) : Guid.Empty;

            return new(isAuthenticated, userGuidId);
        }
    }
}