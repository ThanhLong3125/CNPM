using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend.role;

namespace backend.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(50)]
        public string Full_name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(Role))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; } = Role.Staff;
    }

    public class UpdateUserDto
    {
        public string? Full_name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string Full_name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string Full_name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string? Specialty { get; set; }
    }

    public class ResetPasswordByEmailDto
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
