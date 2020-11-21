using Microsoft.AspNetCore.Mvc;

namespace Resource.Api.Resources
{
    public sealed record ApiResponse<T>(int Status, bool Success, T? Data, object? Error) where T : class
    {
        public bool DetailedError => Error is null or not string;

        public static ApiResponse<T> Parse(ObjectResult objectResult)
        {
            var status  = objectResult.StatusCode!.Value;
            var success = status < 400;
            var data    = success is true ? objectResult.Value as T : null;
            var error   = success is false ? objectResult.Value : null;
            return new(status, success, data, error);
        }
    }
}