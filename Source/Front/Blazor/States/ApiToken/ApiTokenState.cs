using BlazorState;

namespace Blazor.States.ApiToken
{
    public partial class ApiTokenState : State<ApiTokenState>
    {
        public string? Token { get; private set; }

        public override void Initialize()
        {
            Token = null;
        }
    }
}