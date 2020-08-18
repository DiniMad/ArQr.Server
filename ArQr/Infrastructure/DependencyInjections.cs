using ArQr.Models.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ArQr.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            return services;
        }
    }
}