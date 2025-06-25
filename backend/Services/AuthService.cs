using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(Guid id, UpdateUserDto updateDto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> ResetPasswordByEmailAsync(string email, string newPassword);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IAuditService _audit;

        public AuthService(AppDbContext context, IConfiguration config, IAuditService audit)
        {
            _context = context;
            _config = config;
            _audit = audit;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new ApplicationException("Email đã tồn tại.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Full_name = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await LogAsync("Register", dto);

            return GenerateJwtToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new ApplicationException("Email hoặc mật khẩu không hợp lệ.");

            await LogAsync("Login", dto);
            return GenerateJwtToken(user);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new ApplicationException("Không tìm thấy người dùng.");

            await LogAsync("GetProfile", id);
            return MapToUserDto(user);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToUserDto).ToList();
        }

        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new ApplicationException("Không tìm thấy người dùng.");

            if (!string.IsNullOrWhiteSpace(dto.FullName)) user.Full_name = dto.FullName;
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber)) user.PhoneNumber = dto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                if (dto.Password.Length < 6 || dto.Password.Length > 100)
                    throw new ApplicationException("Mật khẩu phải từ 6 đến 100 ký tự.");
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _context.SaveChangesAsync();
            await LogAsync("Update", dto);
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new ApplicationException("Không tìm thấy người dùng.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            await LogAsync("Delete", id);
            return true;
        }

        public async Task<bool> ResetPasswordByEmailAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new ApplicationException("Email không tồn tại.");

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                throw new ApplicationException("Mật khẩu phải có ít nhất 6 ký tự.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        // ========== Private Helpers ==========

        private AuthResponseDto GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(
                _config["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT key is not configured.")
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Full_name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow.AddDays(7),
                FullName = user.Full_name,
                Email = user.Email,
                Role = user.Role
            };
        }

        private UserDto MapToUserDto(User user) => new()
        {
            Id = user.Id,
            FullName = user.Full_name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            Role = user.Role,
            Specialty = user.Specialty
        };

        private async Task LogAsync(string action, object details)
        {
            await _audit.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = action,
                Details = details
            });
        }
    }
}
