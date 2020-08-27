using Microsoft.Extensions.Configuration;

namespace ArQr.FileManagement.Infrastructure
{
    public static class ConfigurationExtension
    {
        private const int MbCoefficient = 1024 * 1024;

        public static long GetImageMaxSizeInByte(this IConfiguration configuration)
            => long.Parse(configuration["ImageMaxSizeInMb"]) * MbCoefficient;

        public static long GetVideoMaxSizeInByte(this IConfiguration configuration)
            => long.Parse(configuration["VideoMaxSizeInMb"]) * MbCoefficient;

        public static string GetArQrApiEndpoint(this IConfiguration configuration)
            => configuration["ArQrApiEndpoint"];
    }
}