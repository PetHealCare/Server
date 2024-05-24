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
    }
}
