using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandlordController : ControllerBase
    {
        private readonly ILandlordRepository _landlordRepository;
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public LandlordController(ILandlordRepository landlordRepository, IHelpRepository helpRepository)
        {
            _landlordRepository = landlordRepository;
            _helpRepository = helpRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetLandlordsAsync()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    return Ok(await _landlordRepository.GetLandlordsAsync());
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetLandlordAsync(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    return Ok(await _landlordRepository.GetLandlordAsync(id));
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLandlordAsync([FromForm] LandlordConstructor landlordConstructor)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    var organizByName = await _helpRepository.GetOrganizationByNameAsync(landlordConstructor.OrganizationName);
                    var customerByName = await _helpRepository.GetCustomerByNameAsync(landlordConstructor.CustomerName);
                    if (organizByName != null && customerByName != null)
                    {
                        var isUserMemberOfTheOrganization = await _helpRepository.IsLandlordAsync(customerByName.User_id);
                        bool isUser = await _helpRepository.IsUserAsync(customerByName.User_id);
                        bool isOrganization = await _helpRepository.IsOrganizationAsync(organizByName.Organization_id);

                        if (isUser == true && isOrganization == true && isUserMemberOfTheOrganization == null)
                        {
                            await _landlordRepository.CreateLandlordAsync(customerByName.User_id,organizByName.Organization_id,customerByName.Full_name);
                            return Ok("Ok");
                        }
                        else
                        {
                            return NotFound("Данные не являются корректными");
                        }
                    }
                    else
                    {
                        return BadRequest("Что-то пошло не так");
                    }
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLandlordAsync(int id, Landlord landlord)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    bool isUser = await _helpRepository.IsUserAsync(landlord.User_id);
                    bool isOrganization = await _helpRepository.IsOrganizationAsync(landlord.Organization_id);

                    if (isUser == true && isOrganization == true)
                    {
                        await _landlordRepository.UpdateLandlordAsync(id,landlord);
                        return Ok("Ok");
                    }
                    else
                    {
                        return NotFound("Данные не являются корректными");
                    }
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
