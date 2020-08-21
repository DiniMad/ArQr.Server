using Microsoft.Extensions.Localization;

namespace ArQr.Infrastructure
{
    public static class LocalizerExtensions
    {
        public static string GetUserManagerCreateError(this IStringLocalizer localizer, string errorCode)
            => localizer[$"UserManagerCreate-{errorCode}"];
    }
}