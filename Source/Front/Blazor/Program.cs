using System.Reflection;
using System.Threading.Tasks;
using Blazor.Helpers;
using Blazored.LocalStorage;
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
            services.AddHttpClient(Configuration.Endpoints().Server.Root);
            services.AddServerEndpoints();
            services.AddAntDesign();
            services.AddBlazorState(options => options.Assemblies = new[] {typeof(Program).GetTypeInfo().Assembly});
            services.AddBlazoredLocalStorage();
            services.AddScoped<JsFunctions>();
        }
    }
}