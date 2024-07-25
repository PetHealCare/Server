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
        public async Task<IActionResult> GetBookings([FromQuery]GetListBookingRequest request)
        {
            try
            {
                var response = await _service.GetBookings(request);
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
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var response = await _service.GetBookingById(id);
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
        [HttpPost("create-booking-service")]
        public async Task<IActionResult> CreateBookingService(BookingServiceRequest booking)
        {
            try
            {
                var response = await _service.CreateBookingWithService(booking);
                if (!response)
                {
                    return NotFound(new { message = "Booking creation failed." });
                }
                return Ok(new { message = "Booking created successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An internal server error occurred." });
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
