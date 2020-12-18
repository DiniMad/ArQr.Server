using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;

namespace Blazor.States.ApiToken
{
    public partial class AuthenticationState
    {
        public class SetTokenHandler : ActionHandler<SetTokenAction>
        {
            private AuthenticationState AuthenticationState => Store.GetState<AuthenticationState>();

            public SetTokenHandler(IStore store) : base(store)
            {
            }

            public override async Task<Unit> Handle(SetTokenAction action, CancellationToken cancellationToken)
            {
                AuthenticationState.Token      = action.Value;
                AuthenticationState.ExpireDate = action.ExpireDate;
                return Unit.Value;
            }
        }
    }
}