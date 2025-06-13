// DTOs/ImageDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UploadImageDto
    {
        [Required(ErrorMessage = "An image file is required.")]
        public IFormFile File { get; set; } = null!; // This will hold the actual image data

        [Required(ErrorMessage = "Diagnosis ID is required.")]
        public Guid DiagnosisId { get; set; }

        public string? ImageName { get; set; }
    }

    public class ChangeImageDto
    {
        [Required(ErrorMessage = "An image file is required.")]
        public IFormFile File { get; set; } = null!; // This will hold the actual image data

        public Guid? DiagnosisId { get; set; }

        public string? ImageName { get; set; }
    }

    public class ImageDto
    {
        public Guid Id { get; set; }
        public DateTime UploadDate { get; set; }
        public string? ImageName { get; set; }

        public string AIAnalysis { get; set; } = string.Empty;
        public Guid DiagnosisId { get; set; }
        public string Path { get; set; } = string.Empty; // Added Path
    }
}
