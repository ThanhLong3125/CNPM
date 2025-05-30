using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Images")] // Name the table "Images" or "PatientImages"
    public class Image // Renamed from DICOMImage
    {
        [Key]
        public Guid Id { get; set; } // Using Guid for primary key, consistent with User model

        [Required]
        public Guid PatientId { get; set; } // Foreign key to the Patient this image belongs to

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!; // Navigation property to the Patient

        // Optional: If an image is directly linked to a specific medical record
        public Guid? MedicalRecordId { get; set; }

        [ForeignKey("MedicalRecordId")]
        public MedicalRecord? MedicalRecord { get; set; }

        [Required]
        [StringLength(255)] // Path where the JPEG file is stored (local or cloud URL)
        public string FilePath { get; set; } = string.Empty;

        [Required]
        public DateTime UploadDate { get; set; } = DateTime.UtcNow; // When the image was uploaded

        // Status of the image in the workflow (e.g., "Pending Review", "Reviewed", "Retake Needed")
        [StringLength(50)]
        public string? Status { get; set; }

        // NEW: Track who uploaded the image (Technician)
        [Required]
        public Guid UploadedByUserId { get; set; }

        [ForeignKey("UploadedByUserId")]
        public User UploadedByUser { get; set; } = null!;

        // NEW: Fields for Doctor's use related to THIS IMAGE
        [StringLength(1000)]
        public string? DoctorImageNotes { get; set; } // Specific notes about this image
    }
}
