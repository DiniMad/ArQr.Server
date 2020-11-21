using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Resource.Api.Resources;

namespace ArQr.Infrastructure
{
    public class ApiResponseResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var objectResult = (context.Result as ObjectResult)!;
            objectResult.Value = ApiResponse<object>.Parse(objectResult);
            await next();
        }
    }
}