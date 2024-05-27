using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetListCustomer()
        {
            var response = _service.GetAll();
            if (response == null)
            {
                return NotFound();

            }
            return Ok(response);

        }
        [HttpPost("login")]
        public IActionResult Login(LoginCustomerRequest loginRequest)
        {
            var response = _service.Login(loginRequest);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest registerRequest) 
        {
            var response = _service.Register(registerRequest);
            if(registerRequest == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
