using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerDto)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new ApplicationException("User with this email already exists");
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                Full_name = registerDto.Full_name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                PhoneNumber = registerDto.PhoneNumber,
                Role = registerDto.Role,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            return GenerateJwtToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new ApplicationException("Invalid email or password");
            }

            // Generate JWT token
            return GenerateJwtToken(user);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            return MapToUserDto(user);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToUserDto).ToList();
        }

        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto updateDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            // Chỉ cập nhật khi có giá trị (không null hoặc trắng)
            if (!string.IsNullOrWhiteSpace(updateDto.Full_name))
            {
                user.Full_name = updateDto.Full_name;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.PhoneNumber))
            {
                user.PhoneNumber = updateDto.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
            {
                if (updateDto.Password.Length < 6 || updateDto.Password.Length > 100)
                {
                    throw new ApplicationException("Password must be between 6 and 100 characters.");
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);
            }


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            // Hard delete - remove user from database
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }


        private AuthResponseDto GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured"));

            string roleName = user.Role.ToString();

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Full_name),
        new Claim(ClaimTypes.Role, roleName)
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow.AddDays(7),
                Full_name = user.Full_name,
                Email = user.Email,
                Role = user.Role
            };
        }


        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Full_name = user.Full_name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Role = user.Role,
            };
        }

        public async Task<bool> ResetPasswordByEmailAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new ApplicationException("Email không tồn tại.");
            }

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                throw new ApplicationException("Mật khẩu phải có ít nhất 6 ký tự.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
