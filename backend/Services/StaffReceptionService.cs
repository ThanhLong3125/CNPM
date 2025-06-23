// Services/StaffReceptionService.cs
using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IStaffReceptionService
    {
        Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
        Task<Patient> UpdatePatientAsync(string idPatient, UpdatePatientDto dto);
        Task<PatientDto> SreachPatientAsync(string idPatient);
        Task<List<Patient>> ListPatientAsync();
        Task<List<User>> ListDoctorAsync();
        Task<string> CreateMedicalRecordAsync(CreateMedicalRecordDto dto);
        Task<List<MedicalRecord>> SearchMedicalRecordsByPatientId(string idPatient);
        Task<MedicalRecord> DetailMediaRecordbyId(string medicalRecordId);
        Task<MedicalRecord> UpdateMediaRecordbyId(string medicalRecordId, UpdateMedicalRecordDto dto);
        Task<string> DeleteMediaRecordbyId(string medicalRecordId);
        Task<List<MedicalRecord>> ShowAllMedicalRecord();
    }

    public class StaffReceptionService : IStaffReceptionService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;

        public StaffReceptionService(AppDbContext context, IConfiguration configuration, IAuditService auditService)
        {
            _context = context;
            _configuration = configuration;
            _auditService = auditService;
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
        {
            var exists = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FullName.ToLower() == dto.FullName.ToLower().Trim() &&
                p.DateOfBirth == dto.DateOfBirth &&
                p.Gender.ToLower() == dto.Gender.ToLower().Trim());

            if (exists != null)
                throw new Exception("Bệnh nhân đã tồn tại.");

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                IdPatient = $"BN{Guid.NewGuid():N}".Substring(0, 6).ToUpper(),
                FullName = dto.FullName.Trim(),
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender.Trim(),
                Email = dto.Email?.Trim(),
                Phone = dto.Phone?.Trim(),
                MedicalHistory = dto.MedicalHistory?.Trim()
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Create Patient",
                Details = dto
            });

            return new PatientDto
            {
                Id = patient.Id,
                PatientID = patient.IdPatient,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Email = patient.Email,
                Phone = patient.Phone
            };
        }

        public async Task<Patient> UpdatePatientAsync(string idPatient, UpdatePatientDto dto)
        {
            var patient = await FindPatientByIdPatientAsync(idPatient);

            if (!string.IsNullOrWhiteSpace(dto.FullName)) patient.FullName = dto.FullName;
            if (dto.DateOfBirth.HasValue) patient.DateOfBirth = dto.DateOfBirth.Value;
            if (!string.IsNullOrWhiteSpace(dto.Gender)) patient.Gender = dto.Gender;
            if (!string.IsNullOrWhiteSpace(dto.Email)) patient.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Phone)) patient.Phone = dto.Phone;
            if (!string.IsNullOrWhiteSpace(dto.MedicalHistory)) patient.MedicalHistory = dto.MedicalHistory;

            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Update Patient",
                Details = dto
            });

            return patient;
        }

        public async Task<PatientDto> SreachPatientAsync(string idPatient)
        {
            var patient = await FindPatientByIdPatientAsync(idPatient);

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Search Patient by IdPatient",
                Details = idPatient
            });

            return new PatientDto
            {
                Id = patient.Id,
                PatientID = patient.IdPatient,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Email = patient.Email,
                Phone = patient.Phone
            };
        }

        public async Task<List<Patient>> ListPatientAsync()
        {
            var patients = await _context.Patients.ToListAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "List Patients",
                Details = patients
            });

            return patients;
        }

        public async Task<List<User>> ListDoctorAsync()
        {
            var doctors = await _context.Users.Where(u => u.Role == Role.Doctor).ToListAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "List Doctors",
                Details = doctors
            });

            return doctors;
        }

        public async Task<string> CreateMedicalRecordAsync(CreateMedicalRecordDto dto)
        {
            var patient = await FindPatientByIdPatientAsync(dto.PatientId);
            var record = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                MedicalRecordId = $"BA{Guid.NewGuid():N}".Substring(0, 6).ToUpper(),
                PatientId = dto.PatientId,
                CreatedDate = DateTime.UtcNow,
                Symptoms = dto.Symptoms.Trim(),
                IsPriority = dto.IsPriority,
                AssignedPhysicianId = dto.AssignedPhysicianId,
                Status = false
            };

            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Create Medical Record",
                Details = dto
            });

            return record.MedicalRecordId;
        }

        public async Task<List<MedicalRecord>> SearchMedicalRecordsByPatientId(string idPatient)
        {
            await FindPatientByIdPatientAsync(idPatient);

            var records = await _context.MedicalRecords
                .Where(r => r.PatientId == idPatient)
                .ToListAsync();

            return records;
        }

        public async Task<MedicalRecord> DetailMediaRecordbyId(string medicalRecordId)
        {
            var record = await _context.MedicalRecords.FirstOrDefaultAsync(r => r.MedicalRecordId == medicalRecordId);
            if (record == null) throw new Exception("Không tìm thấy bệnh án.");
            return record;
        }

        public async Task<MedicalRecord> UpdateMediaRecordbyId(string medicalRecordId, UpdateMedicalRecordDto dto)
        {
            var record = await _context.MedicalRecords.FirstOrDefaultAsync(r => r.MedicalRecordId == medicalRecordId);
            if (record == null) throw new Exception("Không tìm thấy bệnh án.");

            if (!string.IsNullOrWhiteSpace(dto.Symptoms)) record.Symptoms = dto.Symptoms;
            if (!string.IsNullOrWhiteSpace(dto.AssignedPhysicianId)) record.AssignedPhysicianId = dto.AssignedPhysicianId;
            if (dto.IsPriority.HasValue) record.IsPriority = dto.IsPriority.Value;
            record.Status = dto.Status;

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<string> DeleteMediaRecordbyId(string medicalRecordId)
        {
            var record = await _context.MedicalRecords.FirstOrDefaultAsync(r => r.MedicalRecordId == medicalRecordId);
            if (record == null) throw new Exception("Không tìm thấy bệnh án.");
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
            return "Xoá thành công";
        }

        public async Task<List<MedicalRecord>> ShowAllMedicalRecord()
        {
            return await _context.MedicalRecords.ToListAsync();
        }

        private async Task<Patient> FindPatientByIdPatientAsync(string idPatient)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == idPatient);
            if (patient == null) throw new Exception("Không tìm thấy bệnh nhân.");
            return patient;
        }
    }
}
