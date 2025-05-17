using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Age => CalculateAge(DateOfBirth);
        public string Gender { get; set; } = string.Empty;
        public string? ContactInfo { get; set; }
        public string? MedicalHistory { get; set; }
        
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public class CreatePatientDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(200)]
        public string? ContactInfo { get; set; }

        [StringLength(500)]
        public string? MedicalHistory { get; set; }
    }

    public class UpdatePatientDto : CreatePatientDto
    {
    }
}
