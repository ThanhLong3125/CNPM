// Models/Image.cs
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
        public Guid DiagnosisId { get; set; }

        [ForeignKey("DiagnosisId")]
        public Diagnosis Diagnosis { get; set; } = null!;

        [Required]
        public DateTime UploadDate { get; set; }

        public string? AIAnalysis { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
