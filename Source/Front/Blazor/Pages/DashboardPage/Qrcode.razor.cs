using System.Threading.Tasks;
using Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.DashboardPage
{
    public partial class Qrcode
    {
        [Parameter] public  long         Id          { get; set; }
        [Inject]    private JsFunctions  JsFunctions { get; set; }
        private             MarkupString _qrcode;

        protected override async Task OnParametersSetAsync()
        {
            var qrcodeSvg = await JsFunctions.QrcodeSvgAsync(Id.ToString());
            _qrcode = new MarkupString(qrcodeSvg);
        }
    }
}