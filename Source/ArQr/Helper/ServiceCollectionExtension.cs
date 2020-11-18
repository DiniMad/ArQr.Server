using System.Text;
using ArQr.Model;
using Data.Repository;
using Data.Repository.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    }
}