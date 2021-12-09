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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public FeedbackController(IFeedbackRepository feedbackRepository, IHelpRepository helpRepository)
        {
            _feedbackRepository = feedbackRepository;
            _helpRepository = helpRepository;
        }
        
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetFeedbacksAsync()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    var feedbacks = await _feedbackRepository.GetFeedbacksAsync();
                    return Ok(feedbacks);
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
        public async Task<IActionResult> GetFeedbackAsync(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    var feedback = await _feedbackRepository.GetFeedbackAsync(id);
                    return Ok(feedback);
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
        public async Task<IActionResult> CreateFeedbackAsync([FromForm]FeedbackConstructor feedbackConstructor)
        {
            try
            {
                DateTime time = DateTime.Today;
                await _feedbackRepository.CreateFeedbackAsync(UserId,feedbackConstructor.Name,time,feedbackConstructor.Text);
                return Ok("OK");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackAsync(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    await _feedbackRepository.DeleteFeedbackAsync(id);
                    return Ok("Ok");
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
