// Models/Diagnosis.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Diagnosis
    {
        [Key]
        public Guid Id { get; set; }

        [Required]

        public string DiagnosisId { get; set; } = string.Empty;

        [Required]
        public string MedicalRecordId { get; set; } = string.Empty;

        [ForeignKey(nameof(MedicalRecordId))]
        public MedicalRecord? MedicalRecord { get; set; }

        [Required]
        public DateTime DiagnosedDate { get; set; }

        public string? Notes { get; set; }

        public Image? Image { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
