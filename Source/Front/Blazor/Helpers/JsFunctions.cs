using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazor.Helpers
{
    public class JsFunctions
    {
        private readonly IJSRuntime _js;
        private const    string     QrcodeSvgIdentifier = "qrcodeSvg";

        public JsFunctions(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<string> QrcodeSvgAsync(string value)
        {
            return await _js.InvokeAsync<string>(QrcodeSvgIdentifier, value);
        }
    }
}