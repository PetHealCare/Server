using BusinessObjects.Models;
using DTOs.Request.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _service;
        public StaffsController(IStaffService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetStaffs()
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetStaffById(int id)
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       
        [HttpPost]
        public IActionResult CreateStaff(staff staff)
        {
            try
            {
                var response = _service.Create(staff);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public IActionResult UpdateStaff(staff staff)
        {
            try
            {
                var response = _service.Update(staff);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        

        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            try
            {
                var response = _service.Delete(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
