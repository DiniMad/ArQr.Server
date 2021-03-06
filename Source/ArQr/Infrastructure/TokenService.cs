using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArQr.Infrastructure
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions               _tokenOptions;
        private readonly SigningCredentials        _credentials;
        private readonly JwtSecurityTokenHandler   _tokenHandler;
        private readonly TokenValidationParameters _validationParameters;


        public TokenService(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetTokenOption();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.JwtSigningKey));
            _credentials  = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _tokenHandler = new JwtSecurityTokenHandler();
            _validationParameters = new TokenValidationParameters
            {
                ValidateIssuer           = false,
                ValidateAudience         = false,
                ValidateLifetime         = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = securityKey
            };
        }

        public string GenerateJwtToken(IDictionary<string, object> claims)
        {
            var token = _tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Expires            = DateTime.UtcNow.AddMinutes(_tokenOptions.JwtExpireIntervalInMinutes),
                SigningCredentials = _credentials,
                Claims             = claims
            });
            return _tokenHandler.WriteToken(token);
        }

        public bool ValidateJwtToken(string token)
        {
            try
            {
                _tokenHandler.ValidateToken(token, _validationParameters, out _);
                return true;
            }
            catch (SecurityTokenValidationException)
            {
                return false;
            }
        }

        public UserRefreshToken GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var       randomBytes              = new byte[32];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new UserRefreshToken
            {
                Token      = Convert.ToBase64String(randomBytes),
                ExpireDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenExpireIntervalInDays)
            };
        }
    }
}