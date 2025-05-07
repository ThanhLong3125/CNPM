using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed admin user
            var adminId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@aidims.com",
                // Default password: Admin123! (hashed)
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
        }
    }
}
