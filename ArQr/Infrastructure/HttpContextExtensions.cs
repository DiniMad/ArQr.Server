using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ArQr.Infrastructure
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContent)
            => httpContent.User.Claims
                          .Single(claim => claim.Properties.Count         > 0 &&
                                           claim.Properties.First().Value == "sub")
                          .Value;
    }
}