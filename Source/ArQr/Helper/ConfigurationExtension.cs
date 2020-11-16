using Microsoft.Extensions.Configuration;

namespace ArQr.Helper
{
    public static class ConfigurationExtension
    {
        public static string GetJwtSigningKey(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("JwtSigningKey");
        }
    }
}