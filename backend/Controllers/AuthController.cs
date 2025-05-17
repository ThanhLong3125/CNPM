using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Đăng ký tài khoản mới")]
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
        [SwaggerOperation(Summary = "Đăng nhập")]
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
        [SwaggerOperation(Summary = "Xem thông tin cá nhân")]
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
        [SwaggerOperation(Summary = "Xem tất cả người dùng(ADMIN)")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/{id}")]
        [SwaggerOperation(Summary = "Xem chi tiết người dùng dựa vào id(ADMIN)")]
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

        [Authorize]
        [HttpPatch("users/{id}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto updateDto)
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
        [SwaggerOperation(Summary = "Xóa người dùng(ADMIN)")]
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
