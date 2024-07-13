using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class DoctorController : ODataController
	{
		private readonly IDoctorService _service;

		public DoctorController(IDoctorService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListDoctor()
		{
			var doctorList = await _service.GetList();

			if (doctorList == null || doctorList.Count == 0)
			{
				return NotFound();
			}
			return Ok(doctorList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var doctor = await _service.GetById(id);
			if (doctor == null)
			{
				return NotFound();
			}
			return Ok(doctor);
		}
	}
}
