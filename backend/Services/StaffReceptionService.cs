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
        Task<Patient> SearchPatientAsync(Guid id);
        Task<List<Patient>> ListPatientAsync();
        Task<List<User>> ListDoctorAsync();
        Task<string> CreateMediaRecordAsync(CreateMedicalRecordDto createMedicalRecordDto);
        Task<List<MedicalRecord>> SearchlistMediaRecordbyId(Guid id);
    }

    public class StaffReceptionService : IStaffReceptionService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;

        public StaffReceptionService(
            AppDbContext context,
            IConfiguration configuration,
            IAuditService auditService
        )
        {
            _context = context;
            _configuration = configuration;
            _auditService = auditService;
        }

        public async Task<string> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FullName.ToLower() == createPatientDto.FullName.ToLower().Trim()
                && p.DateOfBirth.Date == createPatientDto.DateOfBirth.Date
                && p.Gender.ToLower() == createPatientDto.Gender.ToLower().Trim()
            );
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
                MedicalHistory = createPatientDto.MedicalHistory?.Trim(),
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Create Patient",
                    Details = createPatientDto,
                }
            );
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
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Update Patient",
                    Details = updatePatientDto,
                }
            );
            return patient;
        }

        public async Task<Patient> SearchPatientAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                throw new ApplicationException("User not found");
            }
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Search Patient",
                    Details = id,
                }
            );
            return patient;
        }

        public async Task<List<Patient>> ListPatientAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Get list Patient",
                    Details = patients,
                }
            );
            return patients;
        }

        public async Task<string> CreateMediaRecordAsync(
            CreateMedicalRecordDto createMedicalRecordDto
        )
        {
            var patientExists = await _context.Patients.AnyAsync(p =>
                p.Id == createMedicalRecordDto.PatientId
            );
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
            };

            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Create Medical Record",
                    Details = createMedicalRecordDto,
                }
            );

            return medicalRecord.Id.ToString();
        }

        public async Task<List<User>> ListDoctorAsync()
        {
            var doctors = await _context.Users.Where(u => u.Role == Role.Doctor).ToListAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Get Doctor",
                    Details = doctors,
                }
            );
            return doctors;
        }

        public async Task<List<MedicalRecord>> SearchlistMediaRecordbyId(Guid id)
        {
            var records = await _context
                .MedicalRecords.Where(r => r.PatientId == id)
                .Include(r => r.Diagnosis)
                .Include(r => r.Patient)
                .ToListAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Staff",
                    Action = "Create Medical Record",
                    Details = records,
                }
            );
            return records;
        }
    }
}
