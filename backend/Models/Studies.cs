using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Studies")]
    public class Study
    {
        [Key]
        public int Study_ID { get; set; }

        [Required]
        [StringLength(64)]
        public string StudyInstanceUID { get; set; } = string.Empty;

        [Required]
        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient Patient { get; set; } = null!;

        [Required]
        public DateTime Study_Date { get; set; }

        [Required]
        public Guid Referring_Physician_ID { get; set; }

        [ForeignKey("Referring_Physician_ID")]
        public User ReferringPhysician { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(20)]
        public string? Status { get; set; }
    }
}
