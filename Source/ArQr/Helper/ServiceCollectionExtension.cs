using System.IdentityModel.Tokens.Jwt;
using ArQr.Models;
using Data.Repository;
using Data.Repository.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ArQr.Helper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, TokenOption tokenOption)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer           = false,
                        ValidateAudience         = false,
                        ValidateLifetime         = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey         = tokenOption.GetSecurityKey()
                    });

            return services;
        }


        public static IServiceCollection AddTheLocalization(this IServiceCollection services)
        {
            const string englishCulture    = "en-US";
            const string persianCulture    = "fa-IR";
            string[]     supportedCultures = {englishCulture, persianCulture};

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(englishCulture);
                options.AddSupportedCultures(supportedCultures);
                options.AddSupportedUICultures(supportedCultures);
            });

            return services;
        }
    }
}