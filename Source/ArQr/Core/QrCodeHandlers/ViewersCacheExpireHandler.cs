using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using MediatR;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record ViewersCacheExpireRequest(long QrCodeId) : IRequest;

    public class ViewersCacheExpireHandler : IRequestHandler<ViewersCacheExpireRequest>
    {
        private readonly ICacheService _cacheService;

        public ViewersCacheExpireHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(ViewersCacheExpireRequest request, CancellationToken cancellationToken)
        {
            var qrCodeId = request.QrCodeId;

            return Unit.Value;
        }
    }
}