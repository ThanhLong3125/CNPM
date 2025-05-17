using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class MedicalRecord
    {
        [Key]
        [Column("Record_ID")]
        public int Id { get; set; }

        [Required]
        [Column("Patient_ID")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("Symptoms")]
        public string Symptoms { get; set; } = string.Empty;

        [Column("AssignedPhysicianID")]
        public string? AssignedPhysicianId { get; set; }

        [Required]
        [Column("Status")]
        public string Status { get; set; } = "Pending";

        [Column("IsPriority")]
        public bool IsPriority { get; set; } = false;
    }
}
