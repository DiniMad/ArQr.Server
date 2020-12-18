using System;
using System.Threading.Tasks;
using Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.DashboardPage
{
    public partial class Qrcode
    {
        [Parameter] public  long         Id          { get; set; }
        [Parameter] public  string       Title       { get; set; }
        [Inject]    private JsFunctions  JsFunctions { get; set; }
        private static      MarkupString _qrcodeMarkup;

        protected override async Task OnParametersSetAsync()
        {
            var qrcodeSvg = await JsFunctions.QrcodeSvgAsync(Id.ToString());
            _qrcodeMarkup = new MarkupString(qrcodeSvg);
        }
    }
}