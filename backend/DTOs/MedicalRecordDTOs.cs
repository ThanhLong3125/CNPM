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
        public Guid? AssignedPhysicianId { get; set; }
        public string? AssignedPhysicianName { get; set; }
        public bool IsPriority { get; set; }
    }

    public class CreateMedicalRecordDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "Symptoms are required.")]
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
        public string Symptoms { get; set; } = string.Empty;

        // Making this required if a physician must always be assigned at creation
        [Required(ErrorMessage = "Assigned Physician ID is required.")]
        public Guid AssignedPhysicianId { get; set; }

        public bool IsPriority { get; set; } = false; // Default value for new records
        // public int DiagnosisCount { get; set; }
    }

    public class UpdateMedicalRecordDto
    {
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
        public string? Symptoms { get; set; } // Nullable for partial updates

        public Guid? AssignedPhysicianId { get; set; } // Nullable for partial updates

        public bool? IsPriority { get; set; } // Nullable for partial updates
    }

    public class AssignPhysicianDto
    {
        [Required]
        public Guid PhysicianId { get; set; }

        public string? Notes { get; set; }
    }
}
