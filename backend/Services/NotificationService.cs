using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto createDto);
        Task<NotificationDto> GetNotificationByIdAsync(int id);
        Task<PaginatedNotificationsDto> GetUserNotificationsAsync(Guid userId, NotificationFilterDto filter);
        Task<NotificationCountDto> GetUserNotificationCountsAsync(Guid userId);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> MarkAllAsReadAsync(Guid userId);
        Task<bool> DeleteNotificationAsync(int id);
        Task<bool> DeleteAllUserNotificationsAsync(Guid userId);
    }

    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto createDto)
        {
            // Verify user exists
            var user = await _context.Users.FindAsync(createDto.UserId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var notification = new Notification
            {
                User_ID = createDto.UserId,
                Message = createDto.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return MapToNotificationDto(notification);
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            var notification = await _context.Notifications
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Notification_ID == id);

            if (notification == null)
            {
                throw new ApplicationException("Notification not found");
            }

            return MapToNotificationDto(notification);
        }

        public async Task<PaginatedNotificationsDto> GetUserNotificationsAsync(Guid userId, NotificationFilterDto filter)
        {
            // Verify user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var query = _context.Notifications
                .Where(n => n.User_ID == userId);

            // Apply filters
            if (filter.IsRead.HasValue)
            {
                query = query.Where(n => n.IsRead == filter.IsRead.Value);
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(n => n.CreatedAt >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(n => n.CreatedAt <= filter.ToDate.Value);
            }

            // Get total count for pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var pageSize = Math.Max(1, filter.PageSize);
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            var currentPage = Math.Min(Math.Max(1, filter.Page), pageCount);
            var skip = (currentPage - 1) * pageSize;

            // Get paginated notifications
            var notifications = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedNotificationsDto
            {
                Notifications = notifications.Select(MapToNotificationDto).ToList(),
                TotalCount = totalCount,
                PageCount = pageCount,
                CurrentPage = currentPage,
                PageSize = pageSize
            };
        }

        public async Task<NotificationCountDto> GetUserNotificationCountsAsync(Guid userId)
        {
            // Verify user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var totalCount = await _context.Notifications
                .CountAsync(n => n.User_ID == userId);

            var unreadCount = await _context.Notifications
                .CountAsync(n => n.User_ID == userId && !n.IsRead);

            return new NotificationCountDto
            {
                TotalCount = totalCount,
                UnreadCount = unreadCount
            };
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                throw new ApplicationException("Notification not found");
            }

            notification.IsRead = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(Guid userId)
        {
            // Verify user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var unreadNotifications = await _context.Notifications
                .Where(n => n.User_ID == userId && !n.IsRead)
                .ToListAsync();

            if (!unreadNotifications.Any())
            {
                return true; // No unread notifications to mark
            }

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                throw new ApplicationException("Notification not found");
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllUserNotificationsAsync(Guid userId)
        {
            // Verify user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var notifications = await _context.Notifications
                .Where(n => n.User_ID == userId)
                .ToListAsync();

            if (!notifications.Any())
            {
                return true; // No notifications to delete
            }

            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();
            return true;
        }

        private NotificationDto MapToNotificationDto(Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Notification_ID,
                UserId = notification.User_ID,
                Message = notification.Message,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            };
        }
    }
}
