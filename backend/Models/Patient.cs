using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Patient
    {
        [Key]
        [Column("Patient_GUID")]
        public Guid Id { get; set; }

        [Required]
        [Column("Patient_ID")]
        public string IdPatient { get; set; } = string.Empty;

        [Required]
        [Column("Full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column("Gender")]
        public string Gender { get; set; } = string.Empty;

        [Column("Email")]
        public string? Email { get; set; }

        [Column("Phone")]
        public string? Phone { get; set; }

        [Column("MedicalHistory")]
        public string? MedicalHistory { get; set; }

        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
    }
}
