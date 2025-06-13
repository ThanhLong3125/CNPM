using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Path { get; set; } = string.Empty;

        [Required]
        public Guid DiagnosisId { get; set; } // Foreign Key to Diagnosis

        [ForeignKey("DiagnosisId")]
        public Diagnosis? Diagnosis { get; set; } // Navigation property

        [Required]
        public DateTime UploadDate { get; set; }

        public string? AIAnalysis { get; set; }
    }
}
