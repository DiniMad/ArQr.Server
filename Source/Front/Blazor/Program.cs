using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Helpers;
using BlazorState;
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
            services.AddHttpClient(Configuration.Endpoints().Root);
            services.AddServerEndpoints();
            services.AddAutoMapper(typeof(Program));
            services.AddAntDesign();
            services.AddBlazorState(options => options.Assemblies = new[] {typeof(Program).GetTypeInfo().Assembly});
        }
    }
}