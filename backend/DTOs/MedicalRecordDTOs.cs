using System;
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

        public bool IsPriority { get; set; } = false;
    }

    public class UpdateMedicalRecordDto
    {
        [StringLength(1000, ErrorMessage = "Symptoms cannot exceed 1000 characters.")]
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

    public class MedicalRecordWithPatientDto
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string MedicalRecordId { get; set; }
        public string PhysicicanId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? DiagnosisNotes { get; set; }
        public bool status { get; set; }
    }

    public class MedicalRecordWithDoctorDto
    {
        public string MedicalRecordId { get; set; }
        public string PatientId { get; set; }
        public string NamePatient { get; set; }
        public string PhysicianId { get; set; }
        public string DoctorName { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
