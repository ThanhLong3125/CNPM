using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class CreateDiagnosisDto
    {
        [Required]
        public Guid MedicalRecordId { get; set; } // Foreign Key to MedicalRecord

        [Required]
        public DateTime DiagnosedDate { get; set; }

        public string? Notes { get; set; } // Doctor's specific notes for this diagnosis
    }

    public class UpdateDiagnosisDto
    {
        public string? Notes { get; set; } // Doctor's specific notes for this diagnosis
    }
}
