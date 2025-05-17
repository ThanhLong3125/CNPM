using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        [SwaggerOperation(Summary = "Tạo thông báo mới")]
        public async Task<ActionResult<NotificationDto>> CreateNotification(CreateNotificationDto createDto)
        {
            try
            {
                var notification = await _notificationService.CreateNotificationAsync(createDto);
                return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Xem thông báo cụ thể chi tiết ")]
        public async Task<ActionResult<NotificationDto>> GetNotificationById(int id)
        {
            try
            {
                var notification = await _notificationService.GetNotificationByIdAsync(id);

                // Check if the user is authorized to view this notification
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
                {
                    return Unauthorized();
                }

                // Only allow users to view their own notifications or admins to view any notification
                if (notification.UserId != currentUserId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                return Ok(notification);
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("my")]
        [SwaggerOperation(Summary = "Xem tất cả thông báo")]
        public async Task<ActionResult<PaginatedNotificationsDto>> GetMyNotifications([FromQuery] NotificationFilterDto filter)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
            {
                return Unauthorized();
            }

            try
            {
                var notifications = await _notificationService.GetUserNotificationsAsync(currentUserId, filter);
                return Ok(notifications);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary ="Xem thông báo của người dùng (ADMIN)")]
        public async Task<ActionResult<PaginatedNotificationsDto>> GetUserNotifications(Guid userId, [FromQuery] NotificationFilterDto filter)
        {
            try
            {
                var notifications = await _notificationService.GetUserNotificationsAsync(userId, filter);
                return Ok(notifications);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("my/count")]
        [SwaggerOperation(Summary = "Đếm số lượng thông báo (đã đọc/chưa đọc)")]
        public async Task<ActionResult<NotificationCountDto>> GetMyNotificationCounts()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
            {
                return Unauthorized();
            }

            try
            {
                var counts = await _notificationService.GetUserNotificationCountsAsync(currentUserId);
                return Ok(counts);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}/read")]
        [SwaggerOperation(Summary = "Đánh dấu đã đọc")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                // First get the notification to check ownership
                var notification = await _notificationService.GetNotificationByIdAsync(id);

                // Check if the user is authorized to mark this notification as read
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
                {
                    return Unauthorized();
                }

                // Only allow users to mark their own notifications as read or admins to mark any notification
                if (notification.UserId != currentUserId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                var result = await _notificationService.MarkAsReadAsync(id);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("my/read-all")]
        [SwaggerOperation(Summary = "Đánh dấu đọc tất cả")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _notificationService.MarkAllAsReadAsync(currentUserId);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xóa thông báo cụ thể")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                // First get the notification to check ownership
                var notification = await _notificationService.GetNotificationByIdAsync(id);

                // Check if the user is authorized to delete this notification
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
                {
                    return Unauthorized();
                }

                // Only allow users to delete their own notifications or admins to delete any notification
                if (notification.UserId != currentUserId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                var result = await _notificationService.DeleteNotificationAsync(id);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("my/all")]
        [SwaggerOperation(Summary = "Xóa tất cả")]
        public async Task<IActionResult> DeleteAllMyNotifications()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var currentUserId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _notificationService.DeleteAllUserNotificationsAsync(currentUserId);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("user/{userId}/all")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xóa tất cả (ADMIN)")]
        public async Task<IActionResult> DeleteAllUserNotifications(Guid userId)
        {
            try
            {
                var result = await _notificationService.DeleteAllUserNotificationsAsync(userId);
                return Ok(new { success = result });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
