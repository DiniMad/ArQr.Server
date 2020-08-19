using Microsoft.Extensions.Localization;

namespace ArQr.Infrastructure
{
    public static class LocalizerExtensions
    {
        public static string GetUserManagerCreateError(this IStringLocalizer localizer, string errorCode)
        {
            var key = $"UserManagerCreate-{errorCode}";
            return localizer[key];
        }
    }
}