using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ArQr.Model
{
    public class TokenOption
    {
        public string JwtSigningKey                    { get; set; }
        public int    JwtExpireIntervalInMinutes       { get; set; }
        public int    RefreshTokenExpireIntervalInDays { get; set; }

        public SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSigningKey));
        }
    }
}