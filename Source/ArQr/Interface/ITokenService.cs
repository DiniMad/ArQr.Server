using System.Collections.Generic;
using Domain;

namespace ArQr.Interface
{
    public interface ITokenService
    {
        public string           GenerateJwtToken(IDictionary<string, object> claims);
        public bool             ValidateJwtToken(string                      token);
        public UserRefreshToken GenerateRefreshToken();
    }
}