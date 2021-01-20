using System;
using Blazor.Models;
using BlazorState;

namespace Blazor.States.ApiToken
{
    public partial class AuthenticationState
    {
        public class SetTokenAction : IAction
        {
            public string   Value      { get; set; }
            public DateTime ExpireDate { get; set; }
        }
    }
}