using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Add the null-forgiving operator '!'
        public DbSet<User> Users { get; set; } = null!; // Tells the compiler it won't be null
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Define Relationships Explicitly (Recommended for clarity and control) ---

            // Patient to MedicalRecords (One-to-Many)
            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.MedicalRecords)
                .WithOne(mr => mr.Patient)
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete of medical records if patient is deleted

            // MedicalRecord to Images (One-to-Many)
            modelBuilder
                .Entity<MedicalRecord>()
                .HasMany(mr => mr.Images)
                .WithOne(img => img.MedicalRecord)
                .HasForeignKey(img => img.MedicalRecordId)
                .OnDelete(DeleteBehavior.SetNull); // If a medical record is deleted, set Image.MedicalRecordId to NULL (don't delete images)
            // Or .OnDelete(DeleteBehavior.Restrict) if you want to prevent MR deletion if images are attached
            //            // Image to Patient (One-to-Many, direct link)
            modelBuilder
                .Entity<Image>()
                .HasOne(img => img.Patient)
                .WithMany() // Or specify a navigation property if Patient has an ICollection<Image> images (not MedicalRecords related)
                .HasForeignKey(img => img.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete of images if patient is deleted

            // MedicalRecord to User (CreatedByStaff) - One-to-Many
            modelBuilder
                .Entity<MedicalRecord>()
                .HasOne(mr => mr.CreatedByStaff)
                .WithMany() // A User can create many MedicalRecords, but MedicalRecord doesn't necessarily need a collection of all records created by a staff
                .HasForeignKey(mr => mr.CreatedByStaffId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a User if they created records

            // MedicalRecord to User (AssignedPhysician) - One-to-Many
            modelBuilder
                .Entity<MedicalRecord>()
                .HasOne(mr => mr.AssignedPhysician)
                .WithMany() // A User can be assigned many MedicalRecords
                .HasForeignKey(mr => mr.AssignedPhysicianId)
                .IsRequired(false) // Matches the nullable Guid? in model
                .OnDelete(DeleteBehavior.SetNull); // If an assigned physician is deleted, set AssignedPhysicianId to NULL

            // Image to User (UploadedByUser) - One-to-Many
            modelBuilder
                .Entity<Image>()
                .HasOne(img => img.UploadedByUser)
                .WithMany() // A User can upload many Images
                .HasForeignKey(img => img.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a User if they uploaded images

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
