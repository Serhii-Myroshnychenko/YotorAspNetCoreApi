using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;

namespace YotorAspNetCoreApiResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public LoginController(IHelpRepository helpRepository)
        {
            _helpRepository = helpRepository;
        }
        [HttpGet("Info")]
        public async Task<IActionResult> GetCustomerByIdAsync()
        {
            try
            {
                return Ok(await _helpRepository.GetCustomerByIdAsync(UserId));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("IsAdmin")]
        public async Task<IActionResult> IsAdminAsync()
        {
            try
            {
                return Ok(await _helpRepository.IsAdminAsync(UserId));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
