using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Symptoms { get; set; } = string.Empty;
        public string? AssignedPhysicianId { get; set; }
        public string? PhysicianName { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsPriority { get; set; }
    }

    public class CreateMedicalRecordDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Symptoms { get; set; } = string.Empty;

        public string? AssignedPhysicianId { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public bool IsPriority { get; set; } = false;
    }

    public class UpdateMedicalRecordDto
    {
        [StringLength(1000)]
        public string? Symptoms { get; set; }

        public string? AssignedPhysicianId { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        public bool? IsPriority { get; set; }
    }

    public class AssignPhysicianDto
    {
        [Required]
        public string PhysicianId { get; set; } = string.Empty;
        
        public string? Notes { get; set; }
    }
}
