using DTOs.Request.BillRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet]
        public IActionResult GetBills()
        {
            return Ok(_billService.GetBills());
        }
        [HttpGet("{id}")]
        public IActionResult GetBillById([FromRoute] int id)
        {
            return Ok(_billService.GetBillById(id));
        }
        [HttpPost]
        public IActionResult AddBill([FromBody] BillRequest bill)
        {
            return Ok(_billService.AddBill(bill));
        }
        [HttpPut]
        public IActionResult UpdateBill([FromBody] BillRequest bill)
        {
            return Ok(_billService.UpdateBill(bill));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBill([FromRoute] int id)
        {
            return Ok(_billService.DeleteBill(id));
        }
    }
}
