using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Khai báo IdPatient là AlternateKey (unique key)
            modelBuilder.Entity<Patient>()
                .HasAlternateKey(p => p.IdPatient);

            // Configure foreign key từ MedicalRecord.PatientId (string) đến Patient.IdPatient (string)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .HasPrincipalKey(p => p.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .HasPrincipalKey(p => p.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);



            // Diagnosis: cấu hình FK Diagnosis.MedicalRecordId (string) tham chiếu MedicalRecord.MedicalRecordId (string)
            modelBuilder.Entity<Diagnosis>()
                .HasKey(d => d.DiagnosisId);

            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.MedicalRecord)
                .WithMany()  // hoặc .WithMany(mr => mr.Diagnoses) nếu bạn muốn thêm collection
                .HasForeignKey(d => d.MedicalRecordId)
                .HasPrincipalKey(mr => mr.MedicalRecordId);

            // Image: cấu hình khóa chính và FK với Diagnosis
            // Đặt Id (Guid) làm khóa chính
            modelBuilder.Entity<Image>().HasKey(i => i.Id);

            // Đặt ImageId là alternate key (unique constraint)
            modelBuilder.Entity<Image>()
                .HasAlternateKey(i => i.ImageId);

            // Cấu hình quan hệ với Diagnosis
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Diagnosis)
                .WithMany()
                .HasForeignKey(i => i.DiagnosisId)
                .HasPrincipalKey(d => d.DiagnosisId);


            // Seed admin + staff + doctors
            var adminId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                PhysicianId = $"BS{adminId.ToString().Replace("-", "").Substring(0, 6).ToUpper()}",
                Full_name = "Admin",
                Email = "admin@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = role.Role.Admin,
            });

            var staffId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = staffId,
                PhysicianId = $"BS{staffId.ToString().Replace("-", "").Substring(0, 6).ToUpper()}",
                Full_name = "Staff",
                Email = "staff@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Staff123!"),
                Role = role.Role.Staff,
            });

            var doctor1 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = doctor1,
                PhysicianId = $"BS{doctor1.ToString().Replace("-", "").Substring(0, 6).ToUpper()}",
                Full_name = "Thanh Long",
                Email = "doctor1@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = role.Role.Doctor,
                Specialty = "Radiology"
            });

            var doctor2 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = doctor2,
                PhysicianId = $"BS{doctor2.ToString().Replace("-", "").Substring(0, 6).ToUpper()}",
                Full_name = "Hoang Thien",
                Email = "doctor2@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = role.Role.Doctor,
                Specialty = "Cardiology"
            });

            var doctor3 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = doctor3,
                PhysicianId = $"BS{doctor3.ToString().Replace("-", "").Substring(0, 6).ToUpper()}",
                Full_name = "Khang To",
                Email = "doctor3@aidims.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = role.Role.Doctor,
                Specialty = "Neurology"
            });
        }
    }
}
