using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class PatientDto
    {
        public Guid Id { get; set; } // Patient ID should be Guid, not int
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? ContactInfo { get; set; }
        public string? MedicalHistory { get; set; }
        public ICollection<MedicalRecordDto>? MedicalRecords { get; set; }
    }

    public class CreatePatientDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(200)]
        public string? ContactInfo { get; set; }

        [StringLength(500)]
        public string? MedicalHistory { get; set; }
    }

    public class UpdatePatientDto
    {
        [StringLength(100)]
        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        [StringLength(200)]
        public string? ContactInfo { get; set; }

        [StringLength(500)]
        public string? MedicalHistory { get; set; }
    }
}
