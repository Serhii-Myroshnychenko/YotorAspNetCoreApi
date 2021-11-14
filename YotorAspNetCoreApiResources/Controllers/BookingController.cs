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
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public BookingController(IBookingRepository bookingRepository, IHelpRepository helpRepository)
        {
            _bookingRepository = bookingRepository;
            _helpRepository = helpRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if (isAdmin == true)
                {
                    var bookings = await _bookingRepository.GetBookings();
                    return Ok(bookings);
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
        [Authorize]
        public async Task<IActionResult> GetBooking(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if(isAdmin == true)
                {
                    var booking = await _bookingRepository.GetBooking(id);
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromForm]BookingConstructor bookingConstructor)
        {
            try
            {
                var restriction = await _helpRepository.GetRestrictionByCarName(bookingConstructor.Car_name);
                var car = await _helpRepository.GetCarByCarName(bookingConstructor.Car_name);
                if (car != null && restriction!=null)
                {
                    await _bookingRepository.CreateBooking(restriction.Restriction_id, UserId, car.Car_id, null, bookingConstructor.Start_date, bookingConstructor.End_date, false, bookingConstructor.Full_price, bookingConstructor.Start_address, bookingConstructor.End_address);
                    return Ok("Ok");
                }
                else if (car != null && restriction == null)
                {
                    await _bookingRepository.CreateBooking(null, UserId, car.Car_id, null, bookingConstructor.Start_date, bookingConstructor.End_date, false, bookingConstructor.Full_price, bookingConstructor.Start_address, bookingConstructor.End_address);
                    return Ok("Ok");
                }
                else
                {

                    return BadRequest("Что-то пошло нет так");
                }

                
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
