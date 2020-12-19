using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.ApiResources;

namespace Blazor.Helpers
{
    public static class HttpClientExtension
    {
        public static async Task<ApiResponse<T>> PostAsync<T>(this HttpClient client,
                                                              string          url,
                                                              object          request) where T : class
        {
            var result = await client.PostAsJsonAsync(url, request);
            return await result.Content.ReadFromJsonAsync<ApiResponse<T>>() ?? throw new InvalidOperationException();
        }
    }
}