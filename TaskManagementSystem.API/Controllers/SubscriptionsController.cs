using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignSubscription([FromBody] AssignSubscriptionDto dto)
        {
            var result = await _subscriptionsService.AssignSubscriptionAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateSubscription(Guid userId, [FromBody] UpdateSubscriptionDto dto)
        {
            var result = await _subscriptionsService.UpdateSubscriptionAsync(userId, dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var result = await _subscriptionsService.GetAllSubscriptionsAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSubscriptionByUserId(Guid userId)
        {
            var result = await _subscriptionsService.GetSubscriptionByUserIdAsync(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
