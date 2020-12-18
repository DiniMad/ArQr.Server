using System.Threading.Tasks;
using Blazor.Handlers;
using Blazor.Models;
using Blazor.States.ApiToken;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.HomePage
{
    public partial class Home
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public Endpoints         Endpoints         { get; set; }
        [Inject] public ISender           Sender            { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var authState = GetState<AuthenticationState>();

            if (authState.Authenticated is true)
                NavigateToDashboardPage();
            else if (authState.Expired is true) await TrySilentSignin();
        }

        private async Task TrySilentSignin()
        {
            var response = await Sender.Send(new SilentSigninRequest());
            if (response.Success is false) return;

            await Sender.Send(new StoreJwtTokenRequest(response.Data!.Token));
            NavigateToDashboardPage();
        }

        private void NavigateToDashboardPage()
        {
            NavigationManager.NavigateTo(Endpoints.Client.Dashboard);
        }
    }
}