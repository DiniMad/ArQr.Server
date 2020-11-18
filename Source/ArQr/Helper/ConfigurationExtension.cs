using ArQr.Model;
using Microsoft.Extensions.Configuration;

namespace ArQr.Helper
{
    public static class ConfigurationExtension
    {
        public static TokenOption GetTokenOption(this IConfiguration configuration)
        {
            return configuration.GetSection("Token").Get<TokenOption>();
        }
    }
}