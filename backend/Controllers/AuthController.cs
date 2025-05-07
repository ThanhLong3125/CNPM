using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterUserDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
            {
                return Unauthorized();
            }

            try
            {
                var user = await _authService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            try
            {
                var user = await _authService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, RegisterUserDto updateDto)
        {
            try
            {
                var result = await _authService.UpdateUserAsync(id, updateDto);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _authService.DeleteUserAsync(id);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
