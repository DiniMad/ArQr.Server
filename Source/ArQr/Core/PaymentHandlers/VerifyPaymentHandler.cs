using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using ArQr.Models;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Parbad;
using Resource.ResourceFiles;

namespace ArQr.Core.PaymentHandlers
{
    public sealed record VerifyPaymentRequest : IRequest<ActionHandlerResult>;

    public class VerifyPaymentHandler : IRequestHandler<VerifyPaymentRequest, ActionHandlerResult>
    {
        private readonly IOnlinePayment                         _onlinePayment;
        private readonly ICacheService                          _cacheService;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IMapper                                _mapper;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly CacheOptions                           _cacheOptions;

        public VerifyPaymentHandler(IOnlinePayment                         onlinePayment,
                                    ICacheService                          cacheService,
                                    IConfiguration                         configuration,
                                    IStringLocalizer<HttpResponseMessages> responseMessages,
                                    IMapper                                mapper,
                                    IUnitOfWork                            unitOfWork)
        {
            _onlinePayment    = onlinePayment;
            _cacheService     = cacheService;
            _responseMessages = responseMessages;
            _mapper           = mapper;
            _unitOfWork       = unitOfWork;
            _cacheOptions     = configuration.GetCacheOptions();
        }

        public async Task<ActionHandlerResult> Handle(VerifyPaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = await _onlinePayment.FetchAsync();

            if (payment.Status == PaymentFetchResultStatus.AlreadyProcessed)
                return new(StatusCodes.Status409Conflict,
                           _responseMessages[HttpResponseMessages.PaymentAlreadyProcessed].Value);
            if (payment.IsAlreadyVerified)
                return new(StatusCodes.Status409Conflict,
                           _responseMessages[HttpResponseMessages.PaymentAlreadyVerified]
                               .Value);

            var paymentKey    = _cacheOptions.SequenceKeyBuilder(_cacheOptions.PaymentPrefix, payment.TrackingNumber);
            var paymentString = await _cacheService.GetAsync(paymentKey);
            if (paymentString is null)
            {
                var cancelResult = await _onlinePayment.CancelAsync(payment, "Payment expired.");
                if (cancelResult.IsSucceed is true)
                    return new(StatusCodes.Status410Gone,
                               _responseMessages[HttpResponseMessages.PaymentExpired].Value);
                
                return new(StatusCodes.Status500InternalServerError,
                           _responseMessages[HttpResponseMessages.UnhandledException].Value);
            }


            var cachePayment = JsonSerializer.Deserialize<CachePaymentResource>(paymentString);
            if (cachePayment is null)
                return new(StatusCodes.Status500InternalServerError,
                           _responseMessages[HttpResponseMessages.UnhandledException].Value);
            
            if (cachePayment.PriceInRial != payment.Amount)
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.WrongPayment].Value);

            var verifyResult = await _onlinePayment.VerifyAsync(payment);
            if (verifyResult.IsSucceed is false)
                return new(StatusCodes.Status400BadRequest,
                           _responseMessages[HttpResponseMessages.WrongPayment].Value);


            var purchase = _mapper.Map<Purchase>(cachePayment);
            purchase.TransactionCode = verifyResult.TransactionCode;

            await _unitOfWork.PurchaseRepository.InsertAsync(purchase);
            await _unitOfWork.CompleteAsync();

            return new(StatusCodes.Status200OK, _responseMessages[HttpResponseMessages.Done].Value);
        }
    }
}