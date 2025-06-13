// Models/Diagnosis.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Diagnosis
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid MedicalRecordId { get; set; } // Foreign Key to MedicalRecord

        [ForeignKey("MedicalRecordId")]
        public MedicalRecord? MedicalRecord { get; set; } // Navigation property

        [Required]
        public DateTime DiagnosedDate { get; set; }

        public string? Notes { get; set; } // Doctor's specific notes for this diagnosis

        // Optional: Link to the specific Image that informed this diagnosis
        // public Guid? ImageIdThatInformedDiagnosis { get; set; }

        // [ForeignKey("Images")]
        public Image? Images { get; set; }
    }
}
