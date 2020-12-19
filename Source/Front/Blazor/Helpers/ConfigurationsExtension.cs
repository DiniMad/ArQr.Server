using Blazor.Models;
using Microsoft.Extensions.Configuration;

namespace Blazor.Helpers
{
    public static class ConfigurationsExtension
    {
        public static ServerEndpoints Endpoints(this IConfiguration configuration)
        {
            return configuration.GetSection("ServerEndpoints").Get<ServerEndpoints>();
        }
    }
}