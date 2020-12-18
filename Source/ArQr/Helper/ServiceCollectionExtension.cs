using System.IdentityModel.Tokens.Jwt;
using ArQr.Infrastructure;
using ArQr.Interface;
using ArQr.Models;
using Data;
using Data.Repository;
using Data.Repository.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Parbad.Builder;
using Parbad.Storage.EntityFrameworkCore.Builder;
using StackExchange.Redis;

namespace ArQr.Helper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>()
                           .AddTransient<DatabaseMigrator>();
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
                                                              TokenOptions            tokenOptions)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer           = false,
                        ValidateAudience         = false,
                        ValidateLifetime         = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey         = tokenOptions.GetSecurityKey(),
                        RoleClaimType            = "role"
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

            services.AddLocalization();

            return services;
        }

        public static IServiceCollection AddCacheService(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(connectionString));
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }

        public static IServiceCollection AddTheParbad(this IServiceCollection services, string connectionString)
        {
            services.AddParbad()
                    .ConfigureGateways(builder =>
                    {
                        builder.AddParbadVirtual()
                               .WithOptions(options => options.GatewayPath =
                                                           "/MyVirtualGateway");
                    })
                    .ConfigureHttpContext(builder => builder.UseDefaultAspNetCore())
                    .ConfigureStorage(builder =>
                    {
                        builder.UseEfCore(options =>
                        {
                            var assemblyName = typeof(Startup).Assembly.GetName().Name;
                            options.ConfigureDbContext =
                                db => db.UseSqlServer(connectionString,
                                                      sql => sql.MigrationsAssembly(assemblyName));
                        });
                    });

            return services;
        }

        public static IServiceCollection AddTheCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder => builder
                                                                            .AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()));

            return services;
        }
    }
}