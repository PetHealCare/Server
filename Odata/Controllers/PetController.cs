using AutoMapper;
using DTOs.Response.Pet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Options;
using Odata.Service;
using Presentation.Client;
using Repositories.Interface;
using Services.Client;
using Services.Extentions;
using Services.Extentions.Paginate;

namespace Odata.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class PetController : ODataController
	{
		private readonly IPetService _service;

		public PetController(IPetService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetListPet()
		{
			var petList = await _service.GetList();

			if(petList == null || petList.Count == 0)
			{
				return NotFound();
			}	
			return Ok( petList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var pet = await _service.GetById(id);
			if (pet == null)
			{
				return NotFound();
			}
			return Ok(pet);
		}
	}
}
