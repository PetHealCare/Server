using BusinessObjects.Models;
using DTOs.Request.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetListService() 
        {
            try
            {
                var response = _service.GetAll();
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
        public IActionResult GetService(int id)
        {
            try
            {
                var response = _service.Get(id);
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
        public IActionResult CreateService([FromBody] ServiceRequest service)
        {
            try
            {
                var response = _service.Create(service);
                if (response == null)
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
