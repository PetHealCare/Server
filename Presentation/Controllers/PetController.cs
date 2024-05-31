using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs;
using DTOs.Request.Pet;
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

		/// <summary>
		/// Get list pet (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] GetListPetRequest request)
		{
			var response = await _service.GetList(request);
			if (response == null || response.TotalCount == 0)
			{
				return NotFound();
			}
			return Ok(response);
		}

		/// <summary>
		/// Get pet by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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
		
		/// <summary>
		/// Create a pet
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Update a pet
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Delete a Pet
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var result = await _service.Delete(id);

			if (result)
			{
				return Ok(true);
			}
			return NotFound();
		}
	}
}
