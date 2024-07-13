using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class StaffController : ODataController
	{
		private readonly IStaffService _service;

		public StaffController(IStaffService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListStaff()
		{
			var staffList = await _service.GetList();

			if (staffList == null || staffList.Count == 0)
			{
				return NotFound();
			}
			return Ok(staffList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var staff = await _service.GetById(id);
			if (staff == null)
			{
				return NotFound();
			}
			return Ok(staff);
		}
	}
}
