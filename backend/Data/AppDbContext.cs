using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;

        public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<MedicalRecord>().HasQueryFilter(mr => !mr.IsDeleted);
            modelBuilder.Entity<Diagnosis>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Image>().HasQueryFilter(i => !i.IsDeleted);
            // modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

            // modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>(); // Stores the Role enum as a string in the database

            modelBuilder
                .Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient) // A MedicalRecord has one Patient
                .WithMany(p => p.MedicalRecords) // A Patient can have many MedicalRecords
                .HasForeignKey(mr => mr.PatientId); // FK is PatientId on MedicalRecord

            modelBuilder
                .Entity<MedicalRecord>()
                .HasOne(mr => mr.User) // A MedicalRecord has one Assigned User (Physician)
                .WithMany(u => u.MedicalRecords) // A User can have many MedicalRecords assigned (no navigation property on User side)
                .HasForeignKey(mr => mr.AssignedPhysicianId); // FK is AssignedPhysicianId on MedicalRecord

            modelBuilder
                .Entity<MedicalRecord>()
                .HasOne(mr => mr.Diagnosis) // MedicalRecord has one Diagnosis
                .WithOne(d => d.MedicalRecord) // Diagnosis has one MedicalRecord
                .HasForeignKey<Diagnosis>(d => d.MedicalRecordId); // FK is on the Diagnosis entity

            modelBuilder
                .Entity<Diagnosis>()
                .HasOne(d => d.Image) // Diagnosis has one Image
                .WithOne(i => i.Diagnosis) // Image has one Diagnosis
                .HasForeignKey<Image>(i => i.DiagnosisId); // FK is on the Image entity

            // Seed admin user
            var adminId = Guid.NewGuid();
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = adminId,
                        Full_name = "Admin",
                        Email = "admin@aidims.com",
                        // Default password: Admin123! (hashed)
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Role = role.Role.Admin,
                    }
                );
            var staffId = Guid.NewGuid();
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = staffId,
                        Full_name = "Staff",
                        Email = "staff@aidims.com",
                        // Default password: Staff123! (hashed)
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Staff123!"),
                        Role = role.Role.Staff,
                    }
                );

            // Seed doctor users with different specialties
            var doctorId1 = Guid.NewGuid();
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = doctorId1,
                        Full_name = "Thanh Long",
                        Email = "doctor1@aidims.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                        Role = role.Role.Doctor,
                        Specialty = "Radiology",
                    }
                );

            var doctorId2 = Guid.NewGuid();
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = doctorId2,
                        Full_name = "Hoang Thien",
                        Email = "doctor2@aidims.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                        Role = role.Role.Doctor,
                        Specialty = "Cardiology",
                    }
                );

            var doctorId3 = Guid.NewGuid();
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = doctorId3,
                        Full_name = "Khang To",
                        Email = "doctor3@aidims.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                        Role = role.Role.Doctor,
                        Specialty = "Neurology",
                    }
                );
        }
    }
}
