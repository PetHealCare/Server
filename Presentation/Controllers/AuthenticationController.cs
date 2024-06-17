using DTOs;
using DTOs.Request.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;
        public AuthenticationController(IConfiguration configuration, ICustomerService customerService)
        {
            _configuration = configuration;
            _customerService = customerService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
           
            var user = await _customerService.Login(model);
            if (user != null)
            {
                var authClaims = new List<Claim>();
                if (user.Role == 1)
                {
                   
                
                        authClaims.Add(new Claim("Email", user.Email));
                        authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        authClaims.Add(new Claim(ClaimTypes.Role, UserRoles.Staff));
                        authClaims.Add(new Claim("Role", UserRoles.Staff));
                        authClaims.Add(new Claim("UserId", user.UserId.ToString()));

                }
                else if (user.Role == 2)
                {
                    authClaims.Add(new Claim("Email", user.Email));
                    authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    authClaims.Add(new Claim(ClaimTypes.Role, UserRoles.Doctor));
                    authClaims.Add(new Claim("Role", UserRoles.Doctor));
                    authClaims.Add(new Claim("UserId", user.UserId.ToString()));
                }
                else if (user.Role == 3)
                {
                    authClaims.Add(new Claim("Email", user.Email));
                    authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    authClaims.Add(new Claim(ClaimTypes.Role, UserRoles.Customer));
                    authClaims.Add(new Claim("Role", UserRoles.Customer));
                    authClaims.Add(new Claim("UserId", user.UserId.ToString()));
                }
                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
