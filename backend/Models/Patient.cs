using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Patient
    {
        [Key]
        [Column("Patient_ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("Full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column("Gender")]
        public string Gender { get; set; } = string.Empty;

        [Column("ContactInfo")]
        public string? ContactInfo { get; set; }

        [Column("MedicalHistory")]
        public string? MedicalHistory { get; set; }

        // Navigation property
        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
    }
}
