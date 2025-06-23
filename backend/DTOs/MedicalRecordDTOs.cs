// DTOs/MedicalRecordDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Symptoms { get; set; } = string.Empty;
        public string? AssignedPhysicianId { get; set; }
        public bool IsPriority { get; set; }

        public bool Status { get; set; }
    }

    public class CreateMedicalRecordDto
    {
        [Required]
        public string PatientId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Symptoms are required.")]
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
        public string Symptoms { get; set; } = string.Empty;

        public string AssignedPhysicianId { get; set; } = string.Empty;

        public bool IsPriority { get; set; } = false; // Default value for new records
    }

    public class UpdateMedicalRecordDto
    {
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
        public string? Symptoms { get; set; } // Nullable for partial updates

        public string? AssignedPhysicianId { get; set; } = string.Empty;

        public bool? IsPriority { get; set; }

        public bool Status { get; set; } = false;
    }

    public class AssignPhysicianDto
    {
        [Required]
        public string PhysicianId { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}
