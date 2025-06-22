using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.role;
using Microsoft.EntityFrameworkCore;

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

        Task<MedicalRecord> DetailMediaRecordbyId(Guid id);

        Task<MedicalRecord> UpdateMediaRecordbyId(Guid id, UpdateMedicalRecordDto dto);

        Task<string> DeleteMediaRecordbyId(Guid id);
        Task<List<MedicalRecord>> ShowAllMediaRecord();
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
                Email = createPatientDto.Email?.Trim(),
                Phone = createPatientDto.Phone?.Trim(),
                Symptoms = createPatientDto.Symptoms?.Trim(),
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Create Patient",
                Details = createPatientDto,
            });
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

            if (!string.IsNullOrEmpty(updatePatientDto.Email))
                patient.Email = updatePatientDto.Email;

            if (!string.IsNullOrEmpty(updatePatientDto.Phone))
                patient.Phone = updatePatientDto.Phone;

            if (!string.IsNullOrEmpty(updatePatientDto.Symptoms))
                patient.Symptoms = updatePatientDto.Symptoms;

            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Update Patient",
                Details = updatePatientDto,
            });
            return patient;
        }

        public async Task<Patient> SreachPatientAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                throw new ApplicationException("User not found");
            }
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Sreach Patient",
                Details = id,
            });
            return patient;
        }

        public async Task<List<Patient>> ListPatientAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Get list Patient",
                Details = patients,
            });
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
                AssignedPhysicianId = createMedicalRecordDto.AssignedPhysicianId,
                Status = false
            };

            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Create Medical Record",
                Details = createMedicalRecordDto,
            });

            return medicalRecord.Id.ToString();
        }

        public async Task<List<User>> ListDoctorAsync()
        {
            var doctors = await _context.Users
                .Where(u => u.Role == Role.Doctor)
                .ToListAsync();
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Get Doctor",
                Details = doctors,
            });
            return doctors;
        }

        public async Task<List<MedicalRecord>> SreachlistMediaRecordbyId(Guid id)
        {
            var records = await _context.MedicalRecords
                .Where(r => r.PatientId == id)
                .ToListAsync();
            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Create Medical Record",
                Details = records,
            });
            return records;
        }

        public async Task<MedicalRecord> DetailMediaRecordbyId(Guid id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                throw new Exception("Không tồn tại bệnh án với ID này.");
            }

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Detail Medical Record",
                Details = id
            });

            return medicalRecord;
        }

        public async Task<MedicalRecord> UpdateMediaRecordbyId(Guid id, UpdateMedicalRecordDto dto)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecord == null)
            {
                throw new Exception("Medical record not found");
            }

            if (!string.IsNullOrEmpty(dto.Symptoms))
                medicalRecord.Symptoms = dto.Symptoms;

            if (dto.AssignedPhysicianId.HasValue)
                medicalRecord.AssignedPhysicianId = dto.AssignedPhysicianId.Value;

            if (dto.IsPriority.HasValue)
                medicalRecord.IsPriority = dto.IsPriority.Value;

            medicalRecord.Status = dto.Status;

            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Update Medical Record",
                Details = new
                {
                    Id = id,
                    UpdateData = dto
                }
            });

            return medicalRecord;
        }
        public async Task<string> DeleteMediaRecordbyId(Guid id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecord == null)
            {
                throw new Exception("Medical record not found");
            }

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            await _auditService.WriteLogAsync(new WriteLogDto
            {
                User = "Staff",
                Action = "Delete Medical Record",
                Details = id
            });

            return "Xoá thành công";
        }

        public async Task<List<MedicalRecord>> ShowAllMediaRecord()
        {
            var listRecord = await _context.MedicalRecords.ToListAsync();
            return listRecord;
        }

    }
}