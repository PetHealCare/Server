using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs;
using DTOs.Request.Pet;
using DTOs.Response.Pet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Extentions.Paginate;
using Services.Extentions;
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
		[Authorize]		
		[ProducesResponseType(typeof(PetHealthCareResponse<PaginatedList<PetResponse>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] GetListPetRequest request)
		{
			var pets = await _service.GetList(request);
			if (pets.TotalCount <= 0)
			{
				return StatusCode(404, new PetHealthCareResponse<PaginatedList<PetResponse>>(false, "Pets not found", null));
			}
			return Ok(new PetHealthCareResponse<PaginatedList<PetResponse>>(true, "Pets retrieved successfully", pets));
		}

		/// <summary>
		/// Get pet by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<PetResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var pet = await _service.GetById(id);
			if (pet == null)
			{
				return NotFound(new PetHealthCareResponse<PetResponse>(false, "Pet not found", null));
			}
			return Ok(new PetHealthCareResponse<PetResponse>(true, "Pet retrieved successfully", pet));
		}

		/// <summary>
		/// Create a pet
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<PetResponse>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreatePetRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<PetResponse>(false, "Invalid data", null));
			}

			var pet = await _service.Create(request);
			return CreatedAtAction(nameof(GetById), new { id = pet.PetId }, new PetHealthCareResponse<PetResponse>(true, "Pet created successfully", pet));
		}

		/// <summary>
		/// Update a pet
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<PetResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePet(int id, [FromBody] UpdatePetRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<PetResponse>(false, "Invalid data", null));
			}

			if (id != request.PetId)
			{
				return BadRequest(new PetHealthCareResponse<PetResponse>(false, "Pet ID in the request body does not match the ID in the URL.", null));
			}

			var pet = await _service.Update(request);
			if (pet == null)
			{
				return NotFound(new PetHealthCareResponse<PetResponse>(false, "Pet not found", null));
			}
			return Ok(new PetHealthCareResponse<PetResponse>(true, "Pet updated successfully", pet));
		}

		/// <summary>
		/// Delete a Pet
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _service.Delete(id);
			if (!result)
			{
				return NotFound(new PetHealthCareResponse<bool>(false, "Pet not found", false));
			}
			return Ok(new PetHealthCareResponse<bool>(true, "Pet deleted successfully", true));
		}
	}
}
