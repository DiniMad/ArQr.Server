using Microsoft.AspNetCore.Builder;

namespace ArQr.Middlewares
{
    public static class LocalizationMiddleware
    {
        private const           string   EnglishCulture    = "en-US";
        private const           string   PersianCulture    = "fa-IR";
        private static readonly string[] SupportedCultures = {EnglishCulture, PersianCulture};

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            var localizationOptions = new RequestLocalizationOptions()
                                      .SetDefaultCulture(EnglishCulture)
                                      .AddSupportedCultures(SupportedCultures)
                                      .AddSupportedUICultures(SupportedCultures);

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}