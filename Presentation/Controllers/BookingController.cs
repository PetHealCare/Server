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
       
        private readonly IScheduleService _scheduleService;
        public BookingController(IBookingService service,  IScheduleService scheduleService)
        {
            _service = service;
            
            _scheduleService = scheduleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var response = await _service.GetBookings();
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            try
            {
                var response = _service.GetBookingById(id);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingRequest booking)
        {
            try
            {
                var response = await _service.CreateBooking(booking);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost("create-booking-staff")]
        public async Task<IActionResult> CreateBookingOfStaff([FromBody] CreateScheduleAndSlotBookingRequest request)
        {
            try
            {
                var Response = await _service.CreateSlotBookingAndLinkToBooking(request);
                if (Response == false)
                {
                    return NotFound();
                }
                return Ok(Response);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(BookingRequest request)
        {
            try
            {
                var response = await _service.UpdateBooking(request);
                if (response == false)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            try
            {
                var response = await _service.DeleteBooking(bookingId);
                if (response == false)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
