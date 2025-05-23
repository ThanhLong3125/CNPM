using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public interface IStaffReceptionService
    {
        Task<string> CreatePatientAsync(CreatePatientDto createPatientDto);
        Task<Patient> UpdatePatientAsync(Guid id, UpdatePatientDto updatePatientDto);
        Task<Patient> SreachPatientAsync(Guid id);
        Task<List<Patient>> ListPatientAsync();
        Task<List<User>> ListDoctorAsync();
        Task<string> CreateMediaRecordAsync(CreateMedicalRecordDto createMedicalRecordDto);
        Task<List<MedicalRecord>> SreachlistMediaRecordbyId(Guid id);
    }

    public class StaffReceptionService : IStaffReceptionService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public StaffReceptionService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FullName.ToLower() == createPatientDto.FullName.ToLower().Trim() &&
                p.DateOfBirth.Date == createPatientDto.DateOfBirth.Date &&
                p.Gender.ToLower() == createPatientDto.Gender.ToLower().Trim());
            if (existingPatient != null)
            {
                throw new Exception("Bệnh nhân đã tồn tại trong hệ thống.");
            }
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FullName = createPatientDto.FullName.Trim(),
                DateOfBirth = createPatientDto.DateOfBirth,
                Gender = createPatientDto.Gender.Trim(),
                ContactInfo = createPatientDto.ContactInfo?.Trim(),
                MedicalHistory = createPatientDto.MedicalHistory?.Trim()
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient.Id.ToString();
        }

        public async Task<Patient> UpdatePatientAsync(Guid id, UpdatePatientDto updatePatientDto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                throw new Exception("Không tồn tại bệnh nhân với ID này.");
            }

            // Cập nhật các trường nếu có dữ liệu mới
            if (!string.IsNullOrEmpty(updatePatientDto.FullName))
                patient.FullName = updatePatientDto.FullName;

            if (updatePatientDto.DateOfBirth.HasValue)
                patient.DateOfBirth = updatePatientDto.DateOfBirth.Value;

            if (!string.IsNullOrEmpty(updatePatientDto.Gender))
                patient.Gender = updatePatientDto.Gender;

            if (!string.IsNullOrEmpty(updatePatientDto.ContactInfo))
                patient.ContactInfo = updatePatientDto.ContactInfo;

            if (!string.IsNullOrEmpty(updatePatientDto.MedicalHistory))
                patient.MedicalHistory = updatePatientDto.MedicalHistory;
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient> SreachPatientAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                throw new ApplicationException("User not found");
            }
            return patient;
        }

        public async Task<List<Patient>> ListPatientAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }


        public async Task<string> CreateMediaRecordAsync(CreateMedicalRecordDto createMedicalRecordDto)
        {
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == createMedicalRecordDto.PatientId);
            if (!patientExists)
            {
                throw new Exception("Bệnh nhân không tồn tại.");
            }

            var medicalRecord = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                PatientId = createMedicalRecordDto.PatientId,
                CreatedDate = DateTime.UtcNow,
                Symptoms = createMedicalRecordDto.Symptoms.Trim(),
                IsPriority = createMedicalRecordDto.IsPriority,
                AssignedPhysicianId = createMedicalRecordDto.AssignedPhysicianId
            };

            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();

            return medicalRecord.Id.ToString();
        }

        public async Task<List<User>> ListDoctorAsync()
        {
            var doctors = await _context.Users
                .Where(u => u.Role == Role.Doctor)
                .ToListAsync();
            return doctors;
        }

        public async Task<List<MedicalRecord>> SreachlistMediaRecordbyId(Guid id)
        {
            var records = await _context.MedicalRecords
                .Where(r => r.PatientId == id)
                .ToListAsync();

            return records;
        }

    }
}