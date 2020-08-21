using Microsoft.Extensions.Localization;

namespace ArQr.Localization.ErrorKeys
{
    public static class ProductErrors
    {
        public const string NotFound = "NotFound";

        public static string GetProductError(this IStringLocalizer localizer, string errorCode)
            => localizer[$"Product-{errorCode}"];
    }
}