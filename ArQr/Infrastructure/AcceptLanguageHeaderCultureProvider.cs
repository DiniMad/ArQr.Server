using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace ArQr.Infrastructure
{
    public class AcceptLanguageHeaderCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var userLanguages = httpContext.Request.Headers["Accept-Language"].ToString();
            var firstLanguage = userLanguages.Split(',').FirstOrDefault();
            return !string.IsNullOrWhiteSpace(firstLanguage)
                       ? new ProviderCultureResult(firstLanguage, firstLanguage)
                       : default;
        }
    }
}