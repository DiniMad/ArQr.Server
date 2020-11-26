using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;
using Parbad;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.PaymentHandlers
{
    public sealed record BuildInvoiceRequest(InvoiceResource InvoiceResource) : IRequest<ActionHandlerResult>;

    public class BuildInvoiceHandler : IRequestHandler<BuildInvoiceRequest, ActionHandlerResult>
    {
        private const int ThousandTomanToRialCoefficient = 10000;

        private readonly IHttpContextAccessor                   _httpContextAccessor;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IUrlHelper                             _url;
        private readonly IOnlinePayment                         _onlinePayment;
        private readonly ICacheService                          _cacheService;

        public BuildInvoiceHandler(IHttpContextAccessor                   httpContextAccessor,
                                   IUnitOfWork                            unitOfWork,
                                   IStringLocalizer<HttpResponseMessages> responseMessages,
                                   IUrlHelperFactory                      urlHelperFactory,
                                   IActionContextAccessor                 actionContextAccessor,
                                   IOnlinePayment                         onlinePayment,
                                   ICacheService                          cacheService)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _url                 = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            _onlinePayment       = onlinePayment;
            _cacheService        = cacheService;
        }

        public async Task<ActionHandlerResult> Handle(BuildInvoiceRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.GetUserId();
            var user   = await _unitOfWork.UserRepository.GetAsync(userId);
            if (user is null)
                return new(StatusCodes.Status404NotFound,
                           _responseMessages[HttpResponseMessages.UserNotFound].Value);

            var serviceId = request.InvoiceResource.Service;
            var service   = await _unitOfWork.ServiceRepository.GetAsync(serviceId);
            if (service is null || service.Active == false)
                return new(StatusCodes.Status404NotFound, 
                           _responseMessages[HttpResponseMessages.ServiceNotFound].Value);

            var requestedQuantity = request.InvoiceResource.Quantity;
            var totalPriceInRial =
                (long) (service.UnitPriceInThousandToman * requestedQuantity * ThousandTomanToRialCoefficient);

            var verifyUrl = _url.Action("Verify", "Payment", null, _httpContextAccessor.HttpContext.Request.Scheme);

            var invoiceResult = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice
                    .SetAmount(totalPriceInRial)
                    .SetCallbackUrl(verifyUrl)
                    .SetGateway("ParbadVirtual")
                    .UseAutoIncrementTrackingNumber();
            });

            if (invoiceResult.IsSucceed is false)
                return new(StatusCodes.Status500InternalServerError,
                           _responseMessages[HttpResponseMessages.UnhandledException].Value);

            CachePurchaseResource cachePurchase =
                new(invoiceResult.GatewayName, requestedQuantity, 0, userId, serviceId);
            var purchaseString =
                JsonSerializer.Serialize(cachePurchase, new JsonSerializerOptions {IgnoreNullValues = true});
            await _cacheService.SetAsync(purchaseString,
                                         cachePurchase.ToString(),
                                         TimeSpan.FromMinutes(10));

            return new(StatusCodes.Status200OK, invoiceResult.GatewayTransporter.Descriptor);
        }
    }
}