using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Services.Interface;
using Services.Extentions;
using Services.Extentions.Paginate;
using DTOs.Response.Doctor;
using DTOs.Request.Doctor;

namespace Presentation.Controllers.Odata
{
	[Route("api/odata")]
	[ApiController]
	public class DoctorController : ODataController
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
	}
}
