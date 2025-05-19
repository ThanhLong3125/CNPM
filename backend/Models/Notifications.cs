using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int Notification_ID { get; set; }

        [Required]
        public Guid User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User User { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public bool IsRead { get; set; } = false;
    }
}
