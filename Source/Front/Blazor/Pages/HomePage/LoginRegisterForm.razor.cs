using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using AutoMapper;
using Blazor.ApiResources;
using Blazor.Helpers;
using Blazor.Models;
using Microsoft.AspNetCore.Components;
using Blazor.States.ApiToken;
using MediatR;
using BlazorState;

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

        [Inject] private HttpClient          HttpClient   { get; set; }
        [Inject] private ServerEndpoints     Endpoints    { get; set; }
        [Inject] private IMapper             Mapper       { get; set; }
        [Inject] private NotificationService Notification { get; set; }
        [Inject] private ISender             Sender       { get; set; }

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
            var loginResource = Mapper.Map<UserLoginResource>(_loginRegisterModel);
            var response      = await HttpClient.PostAsync<JwtTokenResource>(Endpoints.Login, loginResource);
            Notification.NotifyApiResponse(response, 8);
            if (response.Success is true)
                await Sender.Send(new ApiTokenState.SetTokenAction {Value = response.Data!.Token});
        }

        private async Task Register()
        {
            var registerResource = Mapper.Map<UserRegisterResource>(_loginRegisterModel);
            var response         = await HttpClient.PostAsync<UserResource>(Endpoints.Register, registerResource);
            Notification.NotifyApiResponse(response, 8);
        }
    }
}