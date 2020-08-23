using System.Net;
using System.Text.Json;
using ArQr.Controllers.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ArQr.Infrastructure
{
    public static class Middlewares
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            return app;
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError => appError.Run(async context =>
            {
                string acceptLanguage = context.Request.Headers["Accept-Language"];
                var error = acceptLanguage switch
                {
                    "en-US" => "Unhandled exception.",
                    _       => "خطایی رخ داده است."
                };

                context.Response.StatusCode  = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var response = JsonSerializer.Serialize(ApiResponse.ServerError(error));
                    await context.Response.WriteAsync(response);
                }
            }));
        }
    }
}