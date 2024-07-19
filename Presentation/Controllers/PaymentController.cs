using DTOs.Request.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public IActionResult GetPayments([FromQuery]GetListPaymentRequest request)
        {
            return Ok(_paymentService.GetPayments(request));
        }
        [HttpGet("{id}")]
        public IActionResult GetPaymentById([FromRoute] int id)
        {
            return Ok(_paymentService.GetPaymentById(id));
        }
        [HttpPost]
        public IActionResult AddPayment([FromBody] PaymentRequest payment)
        {
            return Ok(_paymentService.AddPayment(payment));
        }
        [HttpPut]
        public IActionResult UpdatePayment([FromBody] PaymentRequest payment)
        {
            return Ok(_paymentService.UpdatePayment(payment));
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePayment([FromRoute] int id)
        {
            return Ok(_paymentService.DeletePayment(id));
        }
       
    }
}
