using BusinessObjects.Models;
using DTOs.Request.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotBookingsController : ControllerBase
    {
        private readonly ISlotBookingService _slotBookingService;
        public SlotBookingsController(ISlotBookingService slotBookingService)
        {
            _slotBookingService = slotBookingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var slotBookings = await _slotBookingService.GetAll();
                return Ok(slotBookings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var slotBooking = await _slotBookingService.Get(id);
                if (slotBooking == null)
                {
                    return NotFound();
                }
                return Ok(slotBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("slot")]
        public IActionResult GetListSlotBooking(int? doctorId = null, int? serviceId = null, int? scheduleId = null)
        {
            try
            {
                var slotBooking = _slotBookingService.GetListSlotBooking(doctorId, serviceId, scheduleId);
                if (slotBooking == null)
                {
                    return NotFound();
                }
                return Ok(slotBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           ;
        }
        [HttpPost]
        public async Task<IActionResult> Create(SlotBookingRequest request)
        {
            try
            {
                var slotBooking = await _slotBookingService.Create(request);
                return Ok(slotBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(SlotBookingRequest slotBooking)
        {
            try
            {
                var response = await _slotBookingService.Update(slotBooking);
                if (!response)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var response = _slotBookingService.Delete(id);
                if (!response)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
