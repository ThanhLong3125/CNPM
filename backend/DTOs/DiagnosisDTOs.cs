// DTOs/DiagnosisDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class CreateDiagnosisDto
    {
        [Required]
        public string MedicalRecordId { get; set; } = string.Empty; // Foreign Key to MedicalRecord

        [Required]
        public DateTime DiagnosedDate { get; set; }

        public string? Notes { get; set; } // Doctor's specific notes for this diagnosis
    }

    public class UpdateDiagnosisDto
    {
        public string? ImageId { get; set; }
        public string? Notes { get; set; } // Doctor's specific notes for this diagnosis
    }

    public class DiagnosisDto
    {
        [Required]
        public DateTime DiagnosedDate { get; set; }

        [Required]
        public string Notes { get; set; } = null!;

        public string? ImageId { get; set; }
    }

    public class CreateDiagnosisWithOptionalImageDto
{
    public string MedicalRecordId { get; set; } = null!;
    public DateTime DiagnosedDate { get; set; }
    public string? Notes { get; set; }

    // Thông tin ảnh (nếu có)
    public IFormFile? ImageFile { get; set; }
    public string? ImageName { get; set; }
}

}
