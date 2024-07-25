using DTOs.Request.PayOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class PayOSController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly IPaymentService _paymentService;

        public PayOSController(PayOS payOS, IPaymentService paymentService) 
        {
            _payOS = payOS;
            _paymentService = paymentService;

        }

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] CreatePaymentLinkRequest request)
        {
            try
            {
                var payment = _paymentService.GetPaymentById(request.PaymentId);
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData("Payment", 1, Convert.ToInt32(payment.Amount * 1000));
                List<ItemData> items = new List<ItemData> { item };
                PaymentData paymentData = new PaymentData(payment.PaymentId, Convert.ToInt32(payment.Amount * 1000),  "", items, request.cancelUrl, request.returnUrl);

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(new { url = createPayment.checkoutUrl });
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500, new { message = "An error occurred", error = exception.Message });
            }
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(orderId);
                return Ok(paymentLinkInformation);
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok(null);
            }

        }

    }
}
