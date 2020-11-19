using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ArQr.Models
{
    public record TokenOption
    {
        public string JwtSigningKey                    { get; init; }
        public int    JwtExpireIntervalInMinutes       { get; init; }
        public int    RefreshTokenExpireIntervalInDays { get; init; }

        public SymmetricSecurityKey GetSecurityKey()
        {
            return new(Encoding.UTF8.GetBytes(JwtSigningKey));
        }
    }
}