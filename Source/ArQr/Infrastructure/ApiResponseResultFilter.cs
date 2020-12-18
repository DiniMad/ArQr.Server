using System.Threading.Tasks;
using ArQr.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArQr.Infrastructure
{
    public class ApiResponseResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var objectResult = (context.Result as ObjectResult)!;
            objectResult.Value = objectResult.ToApiResponse<object>();
            await next();
        }
    }
}