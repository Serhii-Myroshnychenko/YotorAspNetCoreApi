using BLL.BL;
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
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHelpRepository _helpRepository;
        private readonly BookingСoefficient _bookingСoefficient;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public BookingController(IBookingRepository bookingRepository, IHelpRepository helpRepository)
        {
            _bookingRepository = bookingRepository;
            _helpRepository = helpRepository;
            _bookingСoefficient = new BookingСoefficient();
        }
        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    return Ok(await _bookingRepository.GetBookingsAsync());
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingAsync(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    var booking = await _bookingRepository.GetBookingAsync(id);
                    if(booking != null)
                    {
                        return Ok(booking);
                    }
                    else
                    {
                        return BadRequest("Что-то пошло не так");
                    }
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
        [HttpGet("User")]
        public async Task<IActionResult> GetBookingsByUserIdAsync()
        {
            try
            {
                return Ok(await _bookingRepository.GetBookingsByUserIdAsync(UserId));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Booking(BookingConstructor bookingConstructor)
        {
            try
            {
                var restriction = await _helpRepository.GetRestrictionByCarNameAsync(bookingConstructor.Car_name);
                var car = await _helpRepository.GetCarByCarNameAsync(bookingConstructor.Car_name);
                int countOfDays = bookingConstructor.End_date.Day - bookingConstructor.Start_date.Day;
                double coefficient = _bookingСoefficient.CalculateСoefficient(countOfDays);
                int totalPrice = (int)(bookingConstructor.Full_price * countOfDays * coefficient);

                if (car != null && restriction != null)
                {
                    await _bookingRepository.CreateBookingAsync(restriction.Restriction_id, UserId, car.Car_id, null, bookingConstructor.Start_date, bookingConstructor.End_date, false, totalPrice, bookingConstructor.Start_address, bookingConstructor.End_address);
                    await _helpRepository.UpdateStatusCarAsync(car.Car_id);
                    return Ok("Ok");
                }
                else if (car != null && restriction == null)
                {
                    await _bookingRepository.CreateBookingAsync(null, UserId, car.Car_id, null, bookingConstructor.Start_date, bookingConstructor.End_date, false, totalPrice, bookingConstructor.Start_address, bookingConstructor.End_address);
                    await _helpRepository.UpdateStatusCarAsync(car.Car_id);
                    return Ok("Ok");
                }
                else
                {
                    return NotFound("Что-то пошло нет так");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Params")]
        public async Task<IActionResult> GetBookingByParamsAsync(DateTime start_date, DateTime end_date, string start_address, string end_address)
        {
            try
            {
                return Ok(await _bookingRepository.GetBookingByParamsAsync(start_date, end_date, start_address, end_address));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
