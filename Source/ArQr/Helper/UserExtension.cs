using System.Collections.Generic;
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
            return new Dictionary<string, object>
            {
                {JwtRegisteredClaimNames.Sub, user.Id},
                {JwtRegisteredClaimNames.Email, user.Email},
                {ClaimNamePhoneNumber, user.PhoneNumber},
                {ClaimNameRefreshToken, user.RefreshToken.Token}
            };
        }
    }
}