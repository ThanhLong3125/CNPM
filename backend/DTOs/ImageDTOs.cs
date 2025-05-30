using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid? MedicalRecordId { get; set; }
        public string FilePath { get; set; } = string.Empty; // This will likely be a URL from cloud storage
        public DateTime UploadDate { get; set; }
        public Guid UploadedByUserId { get; set; }
        public string UploadedByUserName { get; set; } = string.Empty; // For display
        public string? DoctorImageNotes { get; set; }
        public string? AIAnalysisResultJson { get; set; } // For displaying AI results
        public string? Status { get; set; }
    }

    public class UploadImageDto // For technicians uploading images
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; }

        public Guid? MedicalRecordId { get; set; } // Optional: link to an existing medical record

        // For file upload, you'll typically use IFormFile in the Controller,
        // so this DTO might primarily hold metadata and the file itself is a separate parameter.
        // If sending base64 or a URL directly, this would be a string.
        // For simplicity assuming the file is handled separately in the Controller, and this DTO provides metadata.
        [Required(ErrorMessage = "File path is required.")] // If client provides URL/path
        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty; // Or a temporary path to be processed

        [StringLength(1000, ErrorMessage = "Doctor image notes cannot exceed 1000 characters.")]
        public string? DoctorImageNotes { get; set; }

        // Status, UploadDate, UploadedByUserId will be set by the backend
    }

    public class UpdateImageDto // For doctors adding notes, or technicians changing status
    {
        [StringLength(1000, ErrorMessage = "Doctor image notes cannot exceed 1000 characters.")]
        public string? DoctorImageNotes { get; set; }
    }

    public class CreateImageAnalysisRequestDto
    {
        // Add properties as needed, e.g.:
        public Guid MedicalRecordId { get; set; }
        public string? RequestNotes { get; set; }
    }
}
