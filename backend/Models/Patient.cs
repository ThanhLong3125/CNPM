using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Patients")]
    public class Patient
    {
        [Key]
        public int Patient_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Full_name { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ContactInfo { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? MedicalHistory { get; set; }
    }
}
