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
        public async Task<IActionResult> GetRestrictionsAsync()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    var restrictions = await _restrictionRepository.GetRestrictionsAsync();
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
        public async Task<IActionResult> GetRestrictionAsync(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    var restriction = await _restrictionRepository.GetRestrictionAsync(id);
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
        public async Task<IActionResult> CreateRestrictionAsync([FromForm]RestrictionConstructor restrictionConstructor)
        {
            try
            {
                var landlord = await _helpRepository.IsLandlordAsync(UserId);
                var isHisOrgan = await _helpRepository.IsThisCarOfHisOrganizationAsync(restrictionConstructor.Name);
                if(landlord != null && isHisOrgan != null && landlord.Organization_id == isHisOrgan.Organization_id)
                {
                    await _restrictionRepository.CreateRestrictionAsync(landlord.Landlord_id,restrictionConstructor.Name,restrictionConstructor.Description);
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
        public async Task<IActionResult> DeleteRestictionAsync(int id)
        {
            try
            {
                var landlord = await _helpRepository.IsLandlordAsync(UserId);
                if(landlord != null)
                {
                    await _restrictionRepository.DeleteRestrictionAsync(id);
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
