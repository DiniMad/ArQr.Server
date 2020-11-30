using System.Collections.Generic;
using System.Security.Claims;
using ArQr.Models;
using Domain;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArQr.Helper
{
    public static class UserExtension
    {
        private const string ClaimNamePhoneNumber  = "phone_number";
        private const string ClaimNameRefreshToken = "refresh_token";

        public static IDictionary<string, object> GetClaims(this User user)
        {
            var claims = new Dictionary<string, object>
            {
                {JwtRegisteredClaimNames.Sub, user.Id},
                {JwtRegisteredClaimNames.Email, user.Email},
                {ClaimNamePhoneNumber, user.PhoneNumber},
                {ClaimNameRefreshToken, user.RefreshToken.Token}
            };

            if (user.Admin is true) claims.Add(ClaimTypes.Role, UserRoles.Admin);

            return claims;
        }
    }
}