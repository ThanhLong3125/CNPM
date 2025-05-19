using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("AI_Analysis_Results")]
    public class AIAnalysisResult
    {
        [Key]
        public int AnalysisResult_ID { get; set; }

        [Required]
        public int Image_ID { get; set; }

        [ForeignKey("Image_ID")]
        public ImageDICOM Image { get; set; } = null!;

        public Guid? Reviewed_By_Physician_ID { get; set; }

        [ForeignKey("Reviewed_By_Physician_ID")]
        public User? ReviewedByPhysician { get; set; }

        [Required]
        public DateTime Analysis_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string AI_Model_Version { get; set; } = string.Empty;

        [Required]
        public string PredictedResult { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        public float ConfidenceScore { get; set; }

        [Required]
        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; } = null!;
    }
}
