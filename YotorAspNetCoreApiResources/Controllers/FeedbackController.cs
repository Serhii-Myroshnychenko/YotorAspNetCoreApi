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
        public async Task<IActionResult> GetFeedbacks()
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if(isAdmin == true)
                {
                    var feedbacks = await _feedbackRepository.GetFeedbacks();
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
        public async Task<IActionResult> GetFeedback(int id)
        {
            try
            {
                bool isAdmin = await _helpRepository.IsAdmin(UserId);
                if (isAdmin == true)
                {
                    var feedback = await _feedbackRepository.GetFeedback(id);
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
        public async Task<IActionResult> CreateFeedback([FromForm]FeedbackConstructor feedbackConstructor)
        {
            try
            {
                DateTime time = DateTime.Now;
                await _feedbackRepository.CreateFeedback(UserId,feedbackConstructor.Name,time,feedbackConstructor.Text);
                return Ok("OK");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
