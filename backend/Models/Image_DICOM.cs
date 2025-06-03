using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Images_DICOM")]
    public class ImageDICOM
    {
        [Key]
        public int Image_ID { get; set; }

        [Required]
        public int PatientID { get; set; } 

        [Required]
        public int Series_ID { get; set; }

        [ForeignKey("Series_ID")]
        public Series Series { get; set; } = null!;

        [Required]
        public int DeviceID { get; set; }

        [ForeignKey("DeviceID")]
        public ImagingDevice Device { get; set; } = null!;

        [Required]
        [StringLength(64)]
        public string SOPInstanceUID { get; set; } = string.Empty;

        [Required]
        public int Instance_Number { get; set; }

        [Required]
        [StringLength(255)]
        public string File_Path { get; set; } = string.Empty;

        [Required]
        public DateTime Acquisition_DateTime { get; set; }

        [StringLength(50)]
        public string? ImageType { get; set; }

        [StringLength(1000)]  
        public string? DoctorComment { get; set; }
    }
}
