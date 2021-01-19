using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;

namespace Blazor.States.ApiToken
{
    public partial class ApiTokenState
    {
        public class SetTokenHandler : ActionHandler<SetTokenAction>
        {
            private ApiTokenState ApiTokenState => Store.GetState<ApiTokenState>();

            public SetTokenHandler(IStore store) : base(store)
            {
            }

            public override async Task<Unit> Handle(SetTokenAction action, CancellationToken cancellationToken)
            {
                ApiTokenState.Token = action.Value;
                return Unit.Value;
            }
        }
    }
}