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
        public string ImageId { get; set; } = string.Empty;

        [Required]
        public string Path { get; set; } = string.Empty;

        [Required]
        public string DiagnosisId { get; set; } = string.Empty;

        [ForeignKey(nameof(DiagnosisId))]
        public Diagnosis Diagnosis { get; set; } = null!;

        [Required]
        public DateTime UploadDate { get; set; }

        public string? AIAnalysis { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? ImageName { get; set; }
    }
}
