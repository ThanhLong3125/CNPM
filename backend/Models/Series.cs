using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Series")]
    public class Series
    {
        [Key]
        public int Series_ID { get; set; }

        [Required]
        public int Study_ID { get; set; }

        [ForeignKey("Study_ID")]
        public Study Study { get; set; } = null!;

        [Required]
        public int Series_Number { get; set; }

        [Required]
        public DateTime Series_Date { get; set; }

        [Required]
        [StringLength(20)]
        public string Modality { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Series_Description { get; set; }

        [StringLength(100)]
        public string? Body_Part_Examined { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
