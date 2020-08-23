using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using ArQr.Data;
using ArQr.IdentityServer;
using ArQr.Infrastructure;
using ArQr.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArQr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration
                                      .GetConnectionString("DefaultConnection"));
            });

            services.AddRepositories();
            services.AddUnitOfWork();

            services.AddDefaultIdentity<ApplicationUser>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount  = false;
                        options.Password.RequireUppercase       = false;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                    .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                    .AddProfileService<ProfileService>();

            services.AddAuthentication()
                    .AddIdentityServerJwt();

            services.AddControllers();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

            services.AddAutoMapper(typeof(Startup));

            services.AddLocalization();
            services.AddCultureProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.ConfigureExceptionHandler();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseLocalization();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}