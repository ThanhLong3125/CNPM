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
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Patient> Patients { get; set; }

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
            });
            var staffId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = staffId,
                Full_name = "Staff",
                Email = "staff@aidims.com",
                // Default password: Staff123! (hashed)
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Staff123!"),
                Role = role.Role.Staff,
            });

            // Seed doctor users with different specialties
            var doctorId1 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = doctorId1,
                Full_name = "Thanh Long",
                Email = "doctor1@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = role.Role.Doctor,
                Specialty = "Radiology",

            });

            var doctorId2 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = doctorId2,
                Full_name = "Hoang Thien",
                Email = "doctor2@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = role.Role.Doctor,
                Specialty = "Cardiology",
            });

            var doctorId3 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = doctorId3,
                Full_name = "Khang To",
                Email = "doctor3@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = role.Role.Doctor,
                Specialty = "Neurology",
            });
        }
    }
}


