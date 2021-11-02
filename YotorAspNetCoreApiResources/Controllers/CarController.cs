using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var cars = await _carRepository.GetCars();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            try
            {
                var car = await _carRepository.GetCar(id);
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
        public async Task<IActionResult> CreateCar([FromForm] CarConstructor carConstructor)    //[FromBody]CarConstructor carConstructor)
        {
            try
            {
                var isLandlord = await _carRepository.IsLandlord(UserId);
                if (isLandlord != null)
                {
                    byte[] imageData = null;
                    using(var binaryReader = new BinaryReader(carConstructor.Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)carConstructor.Photo.Length);
                    }
                    await _carRepository.CreateCar(isLandlord.organization_id, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, true, carConstructor.Type, carConstructor.Price, imageData, carConstructor.Description, carConstructor.Number);
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
    }
}
