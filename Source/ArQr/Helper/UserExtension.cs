using System.Collections.Generic;
using System.Security.Claims;
using Domain;

namespace ArQr.Helper
{
    public static class UserExtension
    {
        public static IDictionary<string, object> GetClaims(this User user)
        {
            return new Dictionary<string, object>
            {
                {"sub", user.Id},
                {ClaimTypes.MobilePhone, user.PhoneNumber},
                {ClaimTypes.Email, user.Email},
                {"refresh token", user.RefreshToken.Token}
            };
        }
    }
}