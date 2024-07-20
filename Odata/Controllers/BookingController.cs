using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class BookingController : ODataController
	{
		private readonly IBookingService _service;

		public BookingController(IBookingService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListBooking()
		{
			var bookingList = await _service.GetList();

			if (bookingList == null || bookingList.Count == 0)
			{
				return NotFound();
			}
			return Ok(bookingList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var booking = await _service.GetById(id);
			if (booking == null)
			{
				return NotFound();
			}
			return Ok(booking);
		}
	}
}
