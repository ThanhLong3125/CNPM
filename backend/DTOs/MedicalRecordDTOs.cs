using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public int PatientId { get; set; }
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

        [Required]
        [StringLength(1000)]
        public string Symptoms { get; set; } = string.Empty;

        public string AssignedPhysicianId { get; set; } = string.Empty;

        public bool IsPriority { get; set; } = false;
    }

    public class UpdateMedicalRecordDto
    {
        [StringLength(1000)]
        public string? Symptoms { get; set; }

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
