using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class ServiceController : ODataController
	{
		private readonly IServiceService _service;

		public ServiceController(IServiceService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListService()
		{
			var serviceList = await _service.GetList();

			if (serviceList == null || serviceList.Count == 0)
			{
				return NotFound();
			}
			return Ok(serviceList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var service = await _service.GetById(id);
			if (service == null)
			{
				return NotFound();
			}
			return Ok(service);
		}
	}
}
