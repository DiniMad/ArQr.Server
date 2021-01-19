using BlazorState;

namespace Blazor.States.ApiToken
{
    public partial class ApiTokenState
    {
        public class SetTokenAction : IAction
        {
            public string Value { get; set; }
        }
    }
}