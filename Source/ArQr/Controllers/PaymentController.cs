using System.Threading.Tasks;
using ArQr.Core.PaymentHandlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.Resources;

namespace ArQr.Controllers
{
    [Route("Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator      _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator           = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RequestInvoice(InvoiceResource invoiceResource)
        {
            var (statusCode, value) = await _mediator.Send(new BuildInvoiceRequest(invoiceResource));
            return StatusCode(statusCode, value);
        }

        [Route("verify")]
        public async Task<ActionResult> VerifyPayment()
        {
            var (statusCode, value) = await _mediator.Send(new VerifyPaymentRequest());
            return StatusCode(statusCode, value);
        }
    }
}