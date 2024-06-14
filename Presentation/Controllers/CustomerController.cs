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
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var response = _service.GetCustomerById(id);
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
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _service.Login(loginRequest);
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest) 
        {
            try {
                var response = _service.Register(registerRequest);
                if (registerRequest == null)
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
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCustomerResquest customerResquest)
        {
            try {
                var response = _service.UpdateProfile(customerResquest);
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
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerRequest request)
        {
            try
            {
                var response = await _service.UpdateCustomer(request);
                if (response == false)
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
    }
}
