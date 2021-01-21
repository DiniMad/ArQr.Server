using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using Blazor.ApiResources;
using Blazor.Handlers;
using Blazor.Helpers;
using Blazor.Models;
using Microsoft.AspNetCore.Components;
using Blazor.States.ApiToken;
using MediatR;
using BlazorState;
using Mapster;

namespace Blazor.Pages.HomePage
{
    public partial class LoginRegisterForm
    {
        #region UI

        private string SubmitText =>
            _loginRegisterType switch
            {
                LoginRegisterType.Login    => "ورود",
                LoginRegisterType.Register => "ساخت",
                _                          => throw new ArgumentOutOfRangeException(nameof(_loginRegisterType))
            };

        #endregion

        [Inject] private HttpClient          HttpClient        { get; set; }
        [Inject] private Endpoints           Endpoints         { get; set; }
        [Inject] private NotificationService Notification      { get; set; }
        [Inject] private ISender             Sender            { get; set; }
        [Inject] private NavigationManager   NavigationManager { get; set; }


        private LoginRegisterType  _loginRegisterType;
        private LoginRegisterModel _loginRegisterModel;

        protected override void OnInitialized()
        {
            _loginRegisterModel = new LoginRegisterModel();
        }

        private async Task Callback()
        {
            if (_loginRegisterType == LoginRegisterType.Login) await Login();
            if (_loginRegisterType == LoginRegisterType.Register) await Register();
        }

        private async Task Login()
        {
            var loginResource = _loginRegisterModel.Adapt<UserLoginResource>();
            var response      = await HttpClient.PostAsync<JwtTokenResource>(Endpoints.Server.Login, loginResource);
            Notification.NotifyApiResponse(response, 8);
            if (response.Success is true)
            {
                await Sender.Send(new StoreJwtTokenRequest(response.Data!.Token));
                NavigationManager.NavigateTo(Endpoints.Client.Dashboard);
            }
        }

        private async Task Register()
        {
            var registerResource = _loginRegisterModel.Adapt<UserRegisterResource>();
            var response = await HttpClient.PostAsync<UserResource>(Endpoints.Server.Register, registerResource);
            Notification.NotifyApiResponse(response, 8);
        }
    }
}