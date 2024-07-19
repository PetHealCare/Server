using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class CustomerController : ODataController
	{
		private readonly ICustomerService _service;

		public CustomerController(ICustomerService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListCustomer()
		{
			var customerList = await _service.GetList();

			if (customerList == null || customerList.Count == 0)
			{
				return NotFound();
			}
			return Ok(customerList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var customer = await _service.GetById(id);
			if (customer == null)
			{
				return NotFound();
			}
			return Ok(customer);
		}
	}
}
