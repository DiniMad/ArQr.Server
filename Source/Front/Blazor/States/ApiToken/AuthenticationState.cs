using System;
using Blazor.Models;
using BlazorState;

namespace Blazor.States.ApiToken
{
    public partial class AuthenticationState : State<AuthenticationState>
    {
        public string?  Token         { get; private set; }
        public bool     Authenticated => Token is not null;
        public DateTime ExpireDate    { get; private set; }
        public bool     Expired       => ExpireDate < DateTime.Now.ToLocalTime();

        public override void Initialize()
        {
            Token = null;
        }
    }
}