using Blazor.Models;
using Microsoft.Extensions.Configuration;

namespace Blazor.Helpers
{
    public static class ConfigurationsExtension
    {
        public static Endpoints Endpoints(this IConfiguration configuration)
        {
            return configuration.GetSection("Endpoints").Get<Endpoints>();
        }
    }
}