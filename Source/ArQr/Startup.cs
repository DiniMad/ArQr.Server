using ArQr.Helper;
using ArQr.Infrastructure;
using ArQr.Interface;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArQr
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<ApiResponseResultFilter>())
                    .AddTheFluentValidation();

            services.AddHttpContextAccessor();
            services.AddUnitOfWork();
            services.AddCacheService(Configuration.GetConnectionString("Redis"));
            services.AddJwtAuthentication(Configuration.GetTokenOption());
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddTheLocalization();
            services.AddTheParbad(Configuration.GetConnectionString("Parbad"));

            services.AddTransient<IResponseMessages, ResponseMessages>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IFileStorage, FileStorage>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // TODO: Remove after developing the Parbad payment functionality. 
                endpoints.MapDefaultControllerRoute();
            });
            // TODO: Remove after developing the Parbad payment functionality. 
            app.UseParbadVirtualGatewayWhenDeveloping();

            app.ApplyDatabasesMigrations();
        }
    }
}