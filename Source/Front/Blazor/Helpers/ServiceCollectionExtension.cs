using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Helpers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHttpClient(this IServiceCollection services, string baseAddress)
        {
            services.AddScoped(_ => new HttpClient
            {
                BaseAddress           = new Uri(baseAddress),
                DefaultRequestHeaders = {AcceptLanguage = {new StringWithQualityHeaderValue("fa-IR")}}
            });

            return services;
        }

        public static IServiceCollection AddServerEndpoints(this IServiceCollection services)
        {
            services.AddTransient(s => s.GetRequiredService<IConfiguration>().Endpoints());

            return services;
        }
    }
}