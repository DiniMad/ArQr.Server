using System.Collections.Generic;
using ArQr.FileManagement.Infrastructure;
using ArQr.FileManagement.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArQr.FileManagement
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
            services.AddControllers();

            var fileSizeLimit = new Dictionary<string, long>
            {
                {"image", Configuration.GetImageMaxSizeInByte()},
                {"video", Configuration.GetVideoMaxSizeInByte()}
            };
            services.AddFileService(new FileServiceOption(fileSizeLimitInByte: fileSizeLimit));

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(Configuration.GetArQrApiEndpoint())
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer",
                                  options =>
                                  {
                                      options.Authority = Configuration.GetArQrApiEndpoint();
                                      options.Audience  = "ArQrAPI";
                                  });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}