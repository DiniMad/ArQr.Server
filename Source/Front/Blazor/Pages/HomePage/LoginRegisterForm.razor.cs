using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.ApiResources;
using Blazor.Helpers;
using Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.HomePage
{
    public partial class LoginRegisterForm
    {
        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        private ServerEndpoints Endpoints { get; set; }

        private LoginRegisterType  _loginRegisterType;
        private LoginRegisterModel _loginRegisterModel;

        private string SubmitText =>
            _loginRegisterType switch
            {
                LoginRegisterType.Login    => "ورود",
                LoginRegisterType.Register => "ساخت",
                _                          => throw new ArgumentOutOfRangeException(nameof(_loginRegisterType))
            };

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
            var loginResource = new UserLoginResource(_loginRegisterModel.PhoneNumber, _loginRegisterModel.Password);
            var response      = await HttpClient.PostAsync<JwtTokenResource>(Endpoints.Login, loginResource);
            Console.WriteLine(response);
        }

        private async Task Register()
        {
            var registerResource =
                new UserRegisterResource(_loginRegisterModel.PhoneNumber, _loginRegisterModel.Password);
            var response = await HttpClient.PostAsync<UserResource>(Endpoints.Register, registerResource);
            Console.WriteLine(response);
        }
    }
}