using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YotorAspNetCoreApi.Contracts;
using YotorAspNetCoreApi.Helpers;
using YotorAspNetCoreApi.Models;

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
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Regist")]

        public async Task<IActionResult> Registration([FromBody] Registration registration)
        {
            try
            {
                bool role = false;
                await _customerRepository.Registration(registration.Full_name, registration.Email, registration.Phone, registration.Password, role);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var user = _customerRepository.GetCustomerAsync(login.Email, login.Password);

            if (user != null)
            {
                //Generate Token 
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

