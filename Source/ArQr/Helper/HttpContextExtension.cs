using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArQr.Helper
{
    public static class HttpContextExtension
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.Claims
                                    .First(claim => claim.Type == JwtRegisteredClaimNames.Sub)
                                    .Value;

            return new Guid(userId);
        }
    }
}