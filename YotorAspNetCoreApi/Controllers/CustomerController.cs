using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using YotorAspNetCoreApi.Contracts;
using YotorAspNetCoreApi.Helpers;
using YotorAspNetCoreApi.Models;
using YotorContext.Models;

namespace YotorAspNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _options;
        private readonly ICustomerRepository _customerRepository;
        
        public CustomerController(IOptions<AuthOptions> options, ICustomerRepository customerRepository)
        {
            _options = options;
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                return Ok(await _customerRepository.GetCustomersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] Registration registration)
        {
            try
            {
                await _customerRepository.Registration(registration.Full_name, registration.Email, registration.Phone, registration.Password, false);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            var user = await _customerRepository.GetCustomerAsync(login.Email, login.Password);

            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new
                {
                    access_token = token
                });
            }
            return NotFound("Неверный логин или пароль");

        }
        private string GenerateJWT(Customer customer)
        {
            var authParams = _options.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("user_id", customer.User_id.ToString()),
                new Claim(ClaimTypes.Name, customer.Full_name),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.MobilePhone, customer.Phone),
                new Claim("is_admin", customer.Is_admin.ToString())

            };
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

