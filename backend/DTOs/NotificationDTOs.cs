using backend.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace backend.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }

    public class CreateNotificationDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;
    }

    public class NotificationFilterDto
    {
        public bool? IsRead { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class NotificationCountDto
    {
        public int TotalCount { get; set; }
        public int UnreadCount { get; set; }
    }

    public class PaginatedNotificationsDto
    {
        public List<NotificationDto> Notifications { get; set; } = new List<NotificationDto>();
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
