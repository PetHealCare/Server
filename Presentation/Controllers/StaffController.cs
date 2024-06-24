using BusinessObjects.Models;
using DTOs.Request.Customer;
using DTOs.Request.Staff;
using DTOs.Response.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Extentions.Paginate;
using Services.Extentions;
using Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
		private readonly IStaffService _service;

		public StaffController(IStaffService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list staff (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<PaginatedList<StaffResponse>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] GetListStaffRequest request)
		{
			var staffs = await _service.GetList(request);
			if (staffs.TotalCount <= 0)
			{
				return StatusCode(404, new PetHealthCareResponse<PaginatedList<StaffResponse>>(false, "Staffs not found", null));
			}
			return Ok(new PetHealthCareResponse<PaginatedList<StaffResponse>>(true, "Staffs retrieved successfully", staffs));
		}

		/// <summary>
		/// Get staff by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<StaffResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var staff = await _service.GetById(id);
			if (staff == null)
			{
				return NotFound(new PetHealthCareResponse<StaffResponse>(false, "Staff not found", null));
			}
			return Ok(new PetHealthCareResponse<StaffResponse>(true, "Staff retrieved successfully", staff));
		}

		/// <summary>
		/// Create a staff
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<StaffResponse>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateStaffRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<StaffResponse>(false, "Invalid data", null));
			}

			var staff = await _service.Create(request);
			return CreatedAtAction(nameof(GetById), new { id = staff.StaffId }, new PetHealthCareResponse<StaffResponse>(true, "Staff created successfully", staff));
		}

		/// <summary>
		/// Update a staff
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(PetHealthCareResponse<StaffResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(PetHealthCareResponse<>), StatusCodes.Status404NotFound)]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<StaffResponse>(false, "Invalid data", null));
			}

			if (id != request.StaffId)
			{
				return BadRequest(new PetHealthCareResponse<StaffResponse>(false, "Staff ID in the request body does not match the ID in the URL.", null));
			}
			var staff = await _service.Update(request);
			if (staff == null)
			{
				return NotFound(new PetHealthCareResponse<StaffResponse>(false, "Staff not found", null));
			}
			return Ok(new PetHealthCareResponse<StaffResponse>(true, "Staff updated successfully", staff));
		}

		/// <summary>
		/// Delete a Staff
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
				return NotFound(new PetHealthCareResponse<bool>(false, "Staff not found", false));
			}
			return Ok(new PetHealthCareResponse<bool>(true, "Staff deleted successfully", true));
		}

	}
}
