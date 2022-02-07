using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public CarController(ICarRepository carRepository, IHelpRepository helpRepository)
        {
            _carRepository = carRepository;
            _helpRepository = helpRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarsAsync()
        {
            try
            {
                return Ok(await _carRepository.GetCarsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarAsync(int id)
        {
            try
            {
                var car = await _carRepository.GetCarAsync(id);
                if (car != null)
                {
                    return Ok(car);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCarAsync([FromForm] CarConstructor carConstructor)
        {
            try
            {
                var isLandlord = await _helpRepository.IsLandlordAsync(UserId);
                if (isLandlord != null)
                {
                    await _carRepository.CreateCarAsync(isLandlord.Organization_id, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, true, carConstructor.Type, carConstructor.Price, null, carConstructor.Description, carConstructor.Number);
                    return Ok("Успешно");
                }
                else
                {
                    return Unauthorized("Вы не являетесь арендодателем");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCarAsync(int id,[FromForm] CarConstructor carConstructor)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    await _carRepository.UpdateCarAsync(id, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, carConstructor.Status, carConstructor.Type, carConstructor.Price, null, carConstructor.Description, carConstructor.Number);
                    return Ok("Успешно");
                }
                return NotFound("Недостаточно прав");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Popularity")]
        public async Task<IActionResult> GetMostPopularCarsAsync()
        {
            try
            {
                return Ok(await _carRepository.GetMostPopularCarsAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
