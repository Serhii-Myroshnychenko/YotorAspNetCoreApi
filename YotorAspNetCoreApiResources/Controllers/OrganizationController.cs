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
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IHelpRepository _helpRepository;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);

        public OrganizationController(IOrganizationRepository organizationRepository, IHelpRepository helpRepository)
        {
            _organizationRepository = organizationRepository;
            _helpRepository = helpRepository;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrganizationAsync(int id)
        {
            try
            {
                var admin = await _helpRepository.IsAdminAsync(UserId);
                if (admin == true)
                {
                    var organizations = await _organizationRepository.GetOrganizationAsync(id);
                    return Ok(organizations);
                }
                else
                {
                    return Unauthorized("Вы не являетесь администратором");
                }

                
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrganizationsAsync()
        {
            try
            {
                var admin = await _helpRepository.IsAdminAsync(UserId);
                if (admin == true)
                {
                    var organizations = await _organizationRepository.GetOrganizationsAsync();
                    return Ok(organizations);
                }
                else
                {
                    return Unauthorized("Вы не являетесь администратором");
                }

                
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateOrganizationAsync(Organization organization)
        {
            try
            {
                var admin = await _helpRepository.IsAdminAsync(UserId);
                if (admin == true)
                {
                    await _organizationRepository.CreateOrganizationAsync(organization);
                    return Ok("Ok");
                }
                else
                {
                    return Unauthorized("Вы не являетесь администратором");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationAsync(int id, Organization organization)
        {
            try
            {
                var admin = await _helpRepository.IsAdminAsync(UserId);
                if(admin == true)
                {
                    await _organizationRepository.EditOrganizationAsync(id,organization);
                    return Ok("Ok");
                }
                else
                {
                    return Unauthorized("Вы не являетесь администратором");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
