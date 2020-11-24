using ArQr.Models;
using Microsoft.Extensions.Configuration;

namespace ArQr.Helper
{
    public static class ConfigurationExtension
    {
        public static TokenOptions GetTokenOption(this IConfiguration configuration)
        {
            return configuration.GetSection("Token").Get<TokenOptions>();
        }
    }
}