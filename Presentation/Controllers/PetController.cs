using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs;
using DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/pet")]
    [ApiController]
    public class PetController : ControllerBase
    {
		private readonly IPetService _service;

		public PetController(IPetService service)
		{
			_service = service;
		}

		[HttpPost()]
		public async Task<IActionResult> Create(CreatePetRequest request)
		{
			var response = await _service.Create(request);
			if (response == null)
			{
				return NotFound();
			}
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _service.GetById(id);
			if (response == null)
			{
				return NotFound();
			}
			return Ok(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePet(int id, [FromBody] UpdatePetRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != request.PetId)
			{
				return BadRequest("Pet ID in the request body does not match the ID in the URL.");
			}

			var response = await _service.Update(request);
			if (response == null)
			{
				return NotFound();
			}

			return Ok(response);
		}


		[HttpGet()]
		public async Task<IActionResult> GetListPet([FromQuery] GetListPetRequest request)
		{
			var response = await _service.GetList(request);
			if (response == null || !response.Any())
			{
				return NotFound();
			}
			return Ok(response);
		}
	}
}
