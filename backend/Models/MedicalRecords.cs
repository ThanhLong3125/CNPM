using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("MedicalRecords")]
    public class MedicalRecord
    {
        [Key]
        public int Record_ID { get; set; }

        [Required]
        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient Patient { get; set; } = null!;

        [ForeignKey("Annotation_ID")]
        public Annotation Annotation { get; set; } = null!;

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Symptoms { get; set; } = string.Empty;

        [Required]
        public int AssignedPhysicianID { get; set; }

        [ForeignKey("AssignedPhysicianID")]
        public User AssignedPhysician { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public bool IsPriority { get; set; } = false;
    }
}
// co notee cua bac si 

