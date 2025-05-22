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
        public Guid? AssignedPhysicianId { get; set; }
        public bool IsPriority { get; set; }
    }

    public class CreateMedicalRecordDto
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Symptoms { get; set; } = string.Empty;

        public Guid AssignedPhysicianId { get; set; }

        public bool IsPriority { get; set; } = false;
    }

    public class UpdateMedicalRecordDto
    {
        [StringLength(1000)]
        public string? Symptoms { get; set; }

        public Guid? AssignedPhysicianId { get; set; }
        
        public bool? IsPriority { get; set; }
    }

    public class AssignPhysicianDto
    {
        [Required]
        public Guid PhysicianId { get; set; }
        
        public string? Notes { get; set; }
    }
}
