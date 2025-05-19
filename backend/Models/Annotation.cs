using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Annotations")]
    public class Annotation
    {
        [Key]
        public int Annotation_ID { get; set; }

        [Required]
        public int Image_ID { get; set; }

        [ForeignKey("Image_ID")]
        public ImageDICOM Image { get; set; } = null!;

        [Required]
        public int Patient_ID { get; set; }

        [ForeignKey("Patient_ID")]
        public Patient Patient { get; set; } = null!;

        [Required]
        public Guid Physician_ID { get; set; }

        [ForeignKey("Physician_ID")]
        public User Physician { get; set; } = null!;

        [Required]
        public DateTime Annotation_Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
