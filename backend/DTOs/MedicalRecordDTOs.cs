using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; } // PatientId should be Guid, not int
        public string PatientName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Guid CreatedByStaffId { get; set; } // Who created it
        public string CreatedByStaffName { get; set; } = string.Empty; // For display
        public string Symptoms { get; set; } = string.Empty;
        public bool IsPriority { get; set; }
        public Guid? AssignedPhysicianId { get; set; }
        public string? AssignedPhysicianName { get; set; } // For display
    }

    public class CreateMedicalRecordDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; } // Patient ID is a GUID

        [Required(ErrorMessage = "Symptoms are required.")]
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
        public string Symptoms { get; set; } = string.Empty;

        // AssignedPhysicianId is optional at creation, assigned later
        public Guid? AssignedPhysicianId { get; set; } // Can be null if not assigned at creation

        public bool IsPriority { get; set; } = false;
    }

    public class UpdateMedicalRecordDto
    {
        // Symptoms might be updated by staff
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
        public string? Symptoms { get; set; }

        public Guid? AssignedPhysicianId { get; set; }

        public bool? IsPriority { get; set; }
    }

    // NEW DTO: For a Doctor to submit a diagnosis
    public class SubmitDiagnosisDto
    {
        [Required(ErrorMessage = "Diagnosis summary is required.")]
        [StringLength(500, ErrorMessage = "Diagnosis summary cannot exceed 500 characters.")]
        public string DiagnosisSummary { get; set; } = string.Empty;
    }

    public class AssignMedicalRecordPhysicianDto
    {
        [Required(ErrorMessage = "Physician ID is required.")]
        public Guid PhysicianId { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}
