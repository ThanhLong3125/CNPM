using System;
using System.Collections.Generic; // Don't forget to add this for ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("MedicalRecords")] // Explicitly setting the table name
    public class MedicalRecord
    {
        [Key]
        [Column("Record_ID")]
        public Guid Id { get; set; } // Primary key for the medical record

        [Required]
        [Column("Patient_ID")]
        public Guid PatientId { get; set; } // Foreign key to the Patient

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!; // Navigation property to the Patient (non-nullable if always exists)

        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // When this record was created/started

        // --- Fields related to "Nhân viên tiếp nhận" (Reception Staff) ---
        // You mentioned "Nhân viên tiếp nhận" creates the record. Let's make sure we track who created it.
        [Required]
        [Column("CreatedByStaffId")]
        public Guid CreatedByStaffId { get; set; } // User ID of the Reception Staff who created it

        [ForeignKey("CreatedByStaffId")]
        public User CreatedByStaff { get; set; } = null!; // Navigation property to the User

        [Required]
        [Column("Symptoms")]
        [StringLength(1000)] // Increased length for symptoms as they can be detailed
        public string Symptoms { get; set; } = string.Empty; // Ghi nhận triệu chứng của bệnh nhân

        [Column("IsPriority")]
        public bool IsPriority { get; set; } = false; // Indicates if this record is high priority

        // --- Fields related to "Bác sĩ" (Doctor) - for diagnosis and review ---
        // This is the doctor to whom the record is transferred for diagnosis
        [Column("AssignedPhysicianId")]
        public Guid? AssignedPhysicianId { get; set; } // Physician/Doctor assigned to this record (nullable if not yet assigned)

        [ForeignKey("AssignedPhysicianId")]
        public User? AssignedPhysician { get; set; } // Navigation property to the assigned Doctor

        [Column("DiagnosisSummary")]
        [StringLength(500)] // A concise summary of the diagnosis
        public string? DiagnosisSummary { get; set; }

        [Column("PhysicianNotes")]
        [StringLength(1000)] // General notes from the doctor
        public string? PhysicianNotes { get; set; }

        // --- Navigation property for related Images ---
        // A medical record can have many images associated with it.
        public ICollection<Image>? Images { get; set; }

        // --- Audit Fields ---
        [Column("LastUpdatedAt")]
        public DateTime? LastUpdatedAt { get; set; }

        [Column("LastUpdatedByUserId")]
        public Guid? LastUpdatedByUserId { get; set; }
    }
}
