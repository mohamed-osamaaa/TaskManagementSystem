using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var user = HttpContext.User;
            var claims = user.Claims;
            
            var userDetails = new
            {
                NameIdentifier = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                Email = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value,
                FullName = user.FindFirst("FullName")?.Value,
                Role = user.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value
            };

            return Ok(userDetails);
        }
    }
}
