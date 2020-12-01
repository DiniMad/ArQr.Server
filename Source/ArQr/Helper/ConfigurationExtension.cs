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

        public static CacheOptions GetCacheOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("CacheOptions").Get<CacheOptions>();
        }

        public static string GetFileStoragePath(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("FileStoragePath");
        }

        public static FileChunksOptions GetFileChunksOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("FileChunksOptions")
                                .Get<FileChunksOptions>(options =>
                                                            options.BindNonPublicProperties = true);
        }
    }
}