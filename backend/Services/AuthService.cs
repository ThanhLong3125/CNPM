using backend.Data;
using backend.DTOs;
using backend.Models;
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
        Task<bool> UpdateUserAsync(Guid id, RegisterUserDto updateDto);
        Task<bool> DeleteUserAsync(Guid id);
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

            // Validate role
            if (registerDto.Role != "User" && registerDto.Role != "Doctor" && registerDto.Role != "Admin")
            {
                throw new ApplicationException("Invalid role. Role must be User, Doctor, or Admin");
            }

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                PhoneNumber = registerDto.PhoneNumber,
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
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

            if (!user.IsActive)
            {
                throw new ApplicationException("User account is deactivated");
            }

            // Update last login
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

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

        public async Task<bool> UpdateUserAsync(Guid id, RegisterUserDto updateDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            // Check if email is being changed and if it's already in use
            if (user.Email != updateDto.Email &&
                await _context.Users.AnyAsync(u => u.Email == updateDto.Email))
            {
                throw new ApplicationException("Email is already in use");
            }

            // Validate role
            if (updateDto.Role != "User" && updateDto.Role != "Doctor" && updateDto.Role != "Admin")
            {
                throw new ApplicationException("Invalid role. Role must be User, Doctor, or Admin");
            }

            // Update user properties
            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.Email = updateDto.Email;
            user.PhoneNumber = updateDto.PhoneNumber;
            user.Role = updateDto.Role;

            // Update password if provided
            if (!string.IsNullOrEmpty(updateDto.Password))
            {
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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }

        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                IsActive = user.IsActive
            };
        }
    }
}
