using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Notification
    {
        [Key]
        [Column("Notification_ID")]
        public int Id { get; set; }

        [Required]
        [Column("User_ID")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [Column("CreateAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("Message")]
        public string Message { get; set; } = string.Empty;

        [Required]
        [Column("IsRead")]
        public bool IsRead { get; set; } = false;
    }
}
