using DTOs.Request.Customer;
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
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var response = _service.GetCustomerById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCustomerRequest loginRequest)
        {
            var response = _service.Login(loginRequest);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest) 
        {
            var response = _service.Register(registerRequest);
            if(registerRequest == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCustomerResquest customerResquest)
        {
            var response = _service.UpdateProfile(customerResquest);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
