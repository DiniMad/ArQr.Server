using System.Threading.Tasks;
using AntDesign;
using Blazor.ApiResources;

namespace Blazor.Helpers
{
    public static class NotificationServiceExtension
    {
        private const string SuccessMessage        = "موفقیت آمیز";
        private const string ErrorMessage          = "خطا";
        private const string SuccessDescription    = "با موفقیت انجام شد.";
        private const string NotificationClassName = "api-response-notification";

        public static void NotifyApiResponse<T>(this NotificationService notification,
                                                     ApiResponse<T>           apiResponse,
                                                     int                      duration = 0)
            where T : class
        {
            var notificationType = apiResponse.Success is true ? NotificationType.Success : NotificationType.Error;
            var message          = apiResponse.Success is true ? SuccessMessage : ErrorMessage;
            var description      = apiResponse.Success is true ? SuccessDescription : apiResponse.NormalizeErrors();
            notification.Open(new NotificationConfig
            {
                Message          = message,
                Description      = description,
                NotificationType = notificationType,
                ClassName        = NotificationClassName,
                Duration         = duration,
            });
        }
    }
}