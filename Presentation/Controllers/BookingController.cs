using BusinessObjects.Models;
using DTOs.Request.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        private readonly ISlotBookingService _slotBookingService;
        private readonly IScheduleService _scheduleService;
        public BookingController(IBookingService service, ISlotBookingService slotBookingService, IScheduleService scheduleService)
        {
            _service = service;
            _slotBookingService = slotBookingService;
            _scheduleService = scheduleService;
        }

        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingRequest booking)
        {
            var response = await _service.CreateBooking(booking);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("create-booking-staff")]
        public async Task<IActionResult> CreateBookingOfStaff([FromBody] CreateScheduleAndSlotBookingRequest request)
        {
           var Response = await _service.CreateSlotBookingAndLinkToBooking(request);
            if(Response == false)
            {
                return NotFound();
            }
           return Ok(Response);
        }


    }
}
