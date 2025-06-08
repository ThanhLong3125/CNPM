using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class MedicalRecord
    {
        [Key]
        [Column("Record_ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("Patient_ID")]
        [JsonIgnore]
        public Guid PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("AssignedPhysicianId")]
        public Guid AssignedPhysicianId { get; set; }

        [Required]
        [Column("Symptoms")]
        public string Symptoms { get; set; } = string.Empty;

        [Column("IsPriority")]
        public bool IsPriority { get; set; } = false;

        // Navigation property
        public ICollection<Diagnosis>? Diagnoses { get; set; }
    }
}
