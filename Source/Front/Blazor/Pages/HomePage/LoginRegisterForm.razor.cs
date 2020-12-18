using System;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor.Pages.HomePage
{
    public partial class LoginRegisterForm
    {
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

        private void Callback(EditContext obj)
        {
            Console.WriteLine(_loginRegisterType);
            Console.WriteLine(_loginRegisterModel);
        }
    }
}