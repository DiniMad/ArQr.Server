using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Helper
{
    public static class ObjectResultExtension
    {
        public static ApiResponse<T> ToApiResponse<T>(this ObjectResult objectResult) where T : class
        {
            var status  = objectResult.StatusCode!.Value;
            var success = status < 400;
            var data    = success is true ? objectResult.Value as T : null;
            var error   = success is false ? objectResult.Value : null;
            return new(status, success, data, error);
        }
    }
}