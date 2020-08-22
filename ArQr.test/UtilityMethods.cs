using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace ArQr.test
{
    public static class UtilityMethods
    {
        public static HttpContext CreateHttpContext(string userId = "")
        {
            var user = new GenericPrincipal(new ClaimsIdentity(new[]
                                            {
                                                new Claim("type", userId)
                                                {
                                                    Properties = {{"key", "sub"}}
                                                }
                                            }),
                                            null);
            var httpContext = new DefaultHttpContext {User = user};
            return httpContext;
        }
    }
}