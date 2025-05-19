using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("ImagingDevices")]
    public class ImagingDevice
    {
        [Key]
        public int Device_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DeviceName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Modality { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        public DateTime Installation_Date { get; set; }
    }
}
