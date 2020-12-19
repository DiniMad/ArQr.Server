using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            Configuration = builder.Configuration;

            RegisterServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(_ => new HttpClient {BaseAddress = new Uri(Configuration.Endpoints().Root)});
            services.AddTransient(s => s.GetRequiredService<IConfiguration>().Endpoints());
            services.AddAutoMapper(typeof(Program));
            services.AddAntDesign();
        }
    }
}