using Data.Repository;
using Data.Repository.Base;
using Microsoft.Extensions.DependencyInjection;

namespace ArQr.Helper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}