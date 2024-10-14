﻿using HairSalon.Contract.Services.Interface;
using HairSalon.Core;
using HairSalon.ModelViews.FeedbackModeViews;
using HairSalon.ModelViews.FeedBackModeViews;
using HairSalon.ModelViews.PaymentModelViews;
using HairSalon.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairSalonBE.API.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<BasePaginatedList<FeedBackModelView>>> GetAllFeedbacks(
                                                                                    int pageNumber = 1,
                                                                                    int pageSize = 5,
                                                                                    [FromQuery] string? id = null,
                                                                                    [FromQuery] string? appointmentId = null)
        {
            var result = await _feedbackService.GetAllFeedbackAsync(pageNumber, pageSize, id, appointmentId);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreatePayment([FromQuery] CreateFeedbackModelView model)
        {
            string result = await _feedbackService.AddFeedbackAsync(model);
            return Ok(new { Message = result });
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<string>> UpdatePayment(string id, [FromQuery] UpdatedFeedbackModelView model)
        {
            string result = await _feedbackService.UpdateFeedbackAsync(id, model);
            return Ok(new { Message = result });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeletePayment(string id)
        {
            string result = await _feedbackService.DeleteFeedbackpAsync(id);
            return Ok(new { Message = result });
        }
    }
}