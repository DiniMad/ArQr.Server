using Microsoft.Extensions.Localization;

namespace ArQr.Localization.ErrorKeys
{
    public static class UserErrors
    {
        public const string UnAuthorize = "UnAuthorize";
        
        public static string GetUserError(this IStringLocalizer localizer, string errorCode)
            => localizer[$"User-{errorCode}"];
    }
}