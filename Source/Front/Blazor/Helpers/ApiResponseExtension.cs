using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Blazor.ApiResources;

namespace Blazor.Helpers
{
    public static class ApiResponseExtension
    {
        public static string NormalizeErrors<T>(this ApiResponse<T> apiResponse) where T : class
        {
            string error;
            if (apiResponse.DetailedError is true)
            {
                var stringBuilder = new StringBuilder("<ul>");
                var rawErrors     = apiResponse.Error!.ToString()!;
                var detailedError =
                    JsonSerializer.Deserialize<DetailedError>(rawErrors,
                                                              new JsonSerializerOptions
                                                                  {PropertyNameCaseInsensitive = true})!;
                foreach (var (_, value) in detailedError.Errors)
                {
                    stringBuilder.Append("<ul>");

                    var errors = value
                                 .Select(s => $"<li>{s}</li>")
                                 .Aggregate((first, second) => $"{first}{second}");
                    stringBuilder.Append(errors);
                    
                    stringBuilder.Append("</ul>");
                }
                stringBuilder.Append("</ul>");

                error = stringBuilder.ToString();
                Console.WriteLine(error);
            }
            else
            {
                error = apiResponse.Error!.ToString()!;
            }

            return error;
        }
    }
}