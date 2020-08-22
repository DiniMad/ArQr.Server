using System.Collections.Generic;
using System.Globalization;
using ArQr.Data.UnitOfWork;
using ArQr.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ArQr.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddCultureProvider(this IServiceCollection services)
        {
            const string persianCulture = "fa-IR";
            const string englishCulture = "en-US";

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(persianCulture),
                new CultureInfo(englishCulture),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(persianCulture, persianCulture);
                options.SupportedCultures     = supportedCultures;
                options.SupportedUICultures   = supportedCultures;
                options.RequestCultureProviders = new IRequestCultureProvider[]
                {
                    new AcceptLanguageHeaderCultureProvider()
                };
            });

            return services;
        }
    }
}