using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }  // Thêm dòng này

        public DbSet<Patient> Patients { get; set; }
        public DbSet<ImageDICOM> ImageDICOM { get; set; }

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
                PhoneNumber = "null",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = Role.Admin,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
        }
    }
}
