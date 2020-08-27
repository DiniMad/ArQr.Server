using ArQr.FileManagement.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ArQr.FileManagement.Infrastructure
{
    public static class Middleware
    {
        public static IServiceCollection AddFileService(this IServiceCollection services, FileServiceOption option)
        {
            services.AddSingleton(provider => new FileService(option));

            return services;
        }
    }
}