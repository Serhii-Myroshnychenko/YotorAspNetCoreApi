using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

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
        public async Task<IActionResult> GetLandlords()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if(isAdmin == true)
                {
                    var landlords = await _landlordRepository.GetLandlords();
                    return Ok(landlords);
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
        public async Task<IActionResult> GetLandlord(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if(isAdmin == true)
                {
                    var landlord = await _landlordRepository.GetLandlord(id);
                    return Ok(landlord);
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
        public async Task<IActionResult> CreateLandlord(Landlord landlord)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if (isAdmin == true)
                {
                    var isUserMemberOfTheOrganization = await _helpRepository.IsLandlord(landlord.User_id); 
                    bool isUser = await _helpRepository.IsUser(landlord.User_id);
                    bool isOrganization = await _helpRepository.IsOrganization(landlord.Organization_id);

                    if(isUser == true && isOrganization == true && isUserMemberOfTheOrganization == null)
                    {
                        await _landlordRepository.CreateLandlord(landlord);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLandlord(int id, Landlord landlord)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if (isAdmin == true)
                {
                    bool isUser = await _helpRepository.IsUser(landlord.User_id);
                    bool isOrganization = await _helpRepository.IsOrganization(landlord.Organization_id);

                    if (isUser == true && isOrganization == true)
                    {
                        await _landlordRepository.UpdateLandlord(id,landlord);
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
