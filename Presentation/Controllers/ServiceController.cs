using BusinessObjects.Models;
using DTOs.Request.Service;
using DTOs.Response.Pet;
using DTOs.Response.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Extentions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetListService() 
        {
            try
            {
                var response = await _service.GetAll();
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            try
            {
                var response = await _service.Get(id);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest service)
        {

			if (!ModelState.IsValid)
			{
				return BadRequest(new PetHealthCareResponse<PetResponse>(false, "Invalid data", null));
			}

			var response = await _service.Create(service);
			return CreatedAtAction(nameof(GetService), new { id = response.ServiceId }, new PetHealthCareResponse<ServiceResponse>(true, "Service created successfully", response));


		}
        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] ServiceRequest service)
        {
            try
            {
                var response = await _service.Update(service);
                if (!response)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            try
            {
                var response = _service.Delete(id);
                if (!response)
                {
                    return BadRequest();
                }
                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
