using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArQr.Helper
{
    public static class HttpContextExtension
    {
        public static long GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return long.Parse(userId);
        }
    }
}