using Microsoft.AspNetCore.Authorization;
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
    public class RestrictionController : ControllerBase
    {
        private readonly IRestrictionRepository _restrictionRepository;
        private readonly IHelpRepository _helpRepository;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);

        public RestrictionController(IRestrictionRepository restrictionRepository, IHelpRepository helpRepository)
        {
            _restrictionRepository = restrictionRepository;
            _helpRepository = helpRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRestrictions()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if(isAdmin == true)
                {
                    var restrictions = await _restrictionRepository.GetRestrictions();
                    return Ok(restrictions);
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
        public async Task<IActionResult> GetRestriction(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if(isAdmin == true)
                {
                    var restriction = await _restrictionRepository.GetRestriction(id);
                    return Ok(restriction);
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
        public async Task<IActionResult> CreateRestriction([FromForm]RestrictionConstructor restrictionConstructor)
        {
            try
            {
                var landlord = await _helpRepository.IsLandlord(UserId);
                var isHisOrgan = await _helpRepository.IsThisCarOfHisOrganization(restrictionConstructor.Name);
                if(landlord != null && isHisOrgan != null && landlord.Organization_id == isHisOrgan.Organization_id)
                {
                    await _restrictionRepository.CreateRestriction(landlord.Landlord_id,restrictionConstructor.Name,restrictionConstructor.Description);
                    return Ok("Ok");
                }
                else
                {
                    return BadRequest("У вас нету доступа к данному автомобилю");
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRestiction(int id)
        {
            try
            {
                var landlord = await _helpRepository.IsLandlord(UserId);
                if(landlord != null)
                {
                    await _restrictionRepository.DeleteRestriction(id);
                    return Ok("Ok");
                }
                else
                {
                    return BadRequest("Вы не являетесь арендодателем");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
