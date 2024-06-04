using BusinessObjects.Models;
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
            var response = _service.GetAll();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            var response = _service.Get(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost]
        public IActionResult CreateService([FromBody] Service service)
        {
            var response = _service.Create(service);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpPut]
        public IActionResult UpdateService([FromBody] Service service)
        {
            var response = _service.Update(service);
            if (!response)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var response = _service.Delete(id);
            if (!response)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
