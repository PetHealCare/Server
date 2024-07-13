using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class ScheduleController : ODataController
	{
		private readonly IScheduleService _service;

		public ScheduleController(IScheduleService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListSchedule()
		{
			var scheduleList = await _service.GetList();

			if (scheduleList == null || scheduleList.Count == 0)
			{
				return NotFound();
			}
			return Ok(scheduleList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var schedule = await _service.GetById(id);
			if (schedule == null)
			{
				return NotFound();
			}
			return Ok(schedule);
		}
	}
}
