using DTOs.Request.Doctor;
using DTOs.Response.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Class;
using Services.Extentions.Paginate;
using Services.Extentions;
using Services.Interface;

namespace Presentation.Controllers
{
	[Route("api/doctor")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly IDoctorService _service;

		public DoctorController(IDoctorService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list doctor (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<PaginatedList<DoctorResponse>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] GetListDoctorRequest request)
		{
			var doctors = await _service.GetList(request);
			if (doctors.TotalCount <= 0)
			{
				return StatusCode(404, new PetHealthCareResponse<PaginatedList<DoctorResponse>>(false, "Doctors not found", null));
			}
			return Ok(new PetHealthCareResponse<PaginatedList<DoctorResponse>>(true, "Doctors retrieved successfully", doctors));
		}

		/// <summary>
		/// Get doctor by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<DoctorResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var doctor = await _service.GetById(id);
			if (doctor == null)
			{
				return NotFound(new PetHealthCareResponse<DoctorResponse>(false, "Doctor not found", null));
			}
			return Ok(new PetHealthCareResponse<DoctorResponse>(true, "Doctor retrieved successfully", doctor));
		}

		/// <summary>
		/// Create a doctor
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<DoctorResponse>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateDoctorRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<DoctorResponse>(false, "Invalid data", null));
			}

			var doctor = await _service.Create(request);
			return CreatedAtAction(nameof(GetById), new { id = doctor.DoctorId }, new PetHealthCareResponse<DoctorResponse>(true, "Doctor created successfully", doctor));
		}

		/// <summary>
		/// Update a doctor
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<DoctorResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<DoctorResponse>(false, "Invalid data", null));
			}

			if (id != request.DoctorId)
			{
				return BadRequest(new PetHealthCareResponse<DoctorResponse>(false, "Doctor ID in the request body does not match the ID in the URL.", null));
			}
			var doctor = await _service.Update(request);
			if (doctor == null)
			{
				return NotFound(new PetHealthCareResponse<DoctorResponse>(false, "Doctor not found", null));
			}
			return Ok(new PetHealthCareResponse<DoctorResponse>(true, "Doctor updated successfully", doctor));
		}

		/// <summary>
		/// Delete a Doctor
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
				return NotFound(new PetHealthCareResponse<bool>(false, "Doctor not found", false));
			}
			return Ok(new PetHealthCareResponse<bool>(true, "Doctor deleted successfully", true));
		}
	}

}
