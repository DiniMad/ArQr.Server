using Microsoft.AspNetCore.Mvc;

namespace ArQr.Controllers.Resources
{
    public class ApiResponse
    {
        public bool   Success { get; }
        public int    Status  { get; }
        public object Data    { get; }
        public string Error   { get; }

        private ApiResponse(bool success, int status, object data, string error)
        {
            Success = success;
            Status  = status;
            Data    = data;
            Error   = error;
        }

        // Succeed Responses
        private static ApiResponse Successful(int status, object data)
            => new ApiResponse(true, status, data, null);

        public static OkObjectResult Ok(object data)
            => new OkObjectResult(Successful(200, data));

        public static CreatedResult Created(string location, object data)
            => new CreatedResult(location, Successful(201, data));


        // Failed Responses
        private static ApiResponse Fail(int status, string error)
            => new ApiResponse(false, status, null, error);

        public static BadRequestObjectResult BadRequest(string error)
            => new BadRequestObjectResult(Fail(400, error));

        public static UnauthorizedObjectResult UnAuthorize(string error)
            => new UnauthorizedObjectResult(Fail(401, error));

        public static NotFoundObjectResult NotFound(string error)
            => new NotFoundObjectResult(Fail(404, error));

        public static ApiResponse ServerError(string error) => Fail(500, error);
    }
}