using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class MedicalRecord
    {
        [Key]
        [Column("Record_ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("MedicalRecordId")]
        public string MedicalRecordId { get; set; } = string.Empty;

        [Required]
        [Column("Patient_ID")]
        public string PatientId { get; set; } = string.Empty;

        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }

        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("AssignedPhysicianId")]
        public string AssignedPhysicianId { get; set; } = string.Empty;

        [Required]
        [Column("Symptoms")]
        public string Symptoms { get; set; } = string.Empty;

        [Column("IsPriority")]
        public bool IsPriority { get; set; } = false;

        [Column("Status")]
        public bool Status { get; set; } = false;
    }
}
