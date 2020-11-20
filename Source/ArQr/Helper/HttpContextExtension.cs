using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArQr.Helper
{
    public static class HttpContextExtension
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return new Guid(userId);
        }
    }
}