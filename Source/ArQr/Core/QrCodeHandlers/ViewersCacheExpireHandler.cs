using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ArQr.Core.QrCodeHandlers
{
    public sealed record ViewersCacheExpireRequest(long QrCodeId) : IRequest<Unit>;

    public class ViewersCacheExpireHandler : IRequestHandler<ViewersCacheExpireRequest, Unit>
    {
        private readonly CacheOptions  _cacheOptions;
        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork   _unitOfWork;

        public ViewersCacheExpireHandler(IConfiguration configuration,
                                         ICacheService  cacheService,
                                         IUnitOfWork    unitOfWork)
        {
            _cacheOptions = configuration.GetCacheOptions();
            _cacheService = cacheService;
            _unitOfWork   = unitOfWork;
        }

        public async Task<Unit> Handle(ViewersCacheExpireRequest request, CancellationToken cancellationToken)
        {
            var qrCodePrefix      = _cacheOptions.QrCodePrefix;
            var viewersListPrefix = _cacheOptions.ViewersListPrefix;

            var qrCodeId          = request.QrCodeId;
            var viewerListKey     = _cacheOptions.SequenceKeyBuilder(qrCodePrefix, viewersListPrefix, qrCodeId);
            var cachedViewersList = (await _cacheService.GetUniqueListAsync(viewerListKey)).ToList();
            if (!cachedViewersList.Any()) return Unit.Value;

            int newViewersCount;
            var persistedViewers =
                (await _unitOfWork.QrCodeViewersRepository.FindAsync(viewer => viewer.QrCodeId == qrCodeId)).ToList();
            if (persistedViewers.Any())
            {
                var persistedViewersId = persistedViewers.Select(viewer => viewer.Id.ToString());
                var newViewers = cachedViewersList
                                 .Where(viewerId => !persistedViewersId.Contains(viewerId))
                                 .Select(viewerId => new QrCodeViewer
                                 {
                                     Id       = long.Parse(viewerId),
                                     QrCodeId = qrCodeId
                                 })
                                 .ToList();

                await _unitOfWork.QrCodeViewersRepository.InsertCollectionAsync(newViewers);
                newViewersCount = newViewers.Count;
            }
            else
            {
                var cachedQrCodeViewers =
                    cachedViewersList.Select(viewerId => new QrCodeViewer
                                     {
                                         Id       = long.Parse(viewerId),
                                         QrCodeId = qrCodeId
                                     })
                                     .ToList();

                await _unitOfWork.QrCodeViewersRepository.InsertCollectionAsync(cachedQrCodeViewers);
                newViewersCount = cachedQrCodeViewers.Count;
            }

            var qrCode = await _unitOfWork.QrCodeRepository.GetAsync(qrCodeId);
            if (qrCode is null) return Unit.Value;
            qrCode.ViewersCount += newViewersCount;
            _unitOfWork.QrCodeRepository.Update(qrCode);

            await _unitOfWork.CompleteAsync();

            await _cacheService.DeleteKeyAsync(viewerListKey);

            return Unit.Value;
        }
    }
}