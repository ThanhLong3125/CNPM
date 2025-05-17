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
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed admin user
            var adminId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                Full_name = "Admin",
                Email = "admin@aidims.com",
                // Default password: Admin123! (hashed)
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = role.Role.Admin,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
        }
    }
}
