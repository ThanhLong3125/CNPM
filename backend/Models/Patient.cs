using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Patient
{
    [Key]
    [Column("Patient_ID")]
    public Guid Id { get; set; }
    [Required]
    public string IdPatient {get; set;} = string.Empty;

        [Required]
        [Column("PatientID")]
        [StringLength(10)]
    
    public string PatientID { get; set; } = string.Empty;

    [Required]
    [Column("Full_name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Column("DateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Column("Gender")]
    public string Gender { get; set; } = string.Empty;

    [Column("Email")]
    public string? Email { get; set; }

    [Column("Phone")]
    public string? Phone { get; set; }

    [Column("MedicaHistory")]
    public string? MedicalHistory { get; set; } = string.Empty;

    public ICollection<MedicalRecord>? MedicalRecords { get; set; }
}

}