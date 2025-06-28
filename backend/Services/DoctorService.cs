// Services/DoctorService.cs
using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IDoctorService
    {
        Task<string> CreateDiagnosisAsync(CreateDiagnosisDto createDiagnosisDto);
        Task<Diagnosis> UpdateDiagnosisAsync(string id, UpdateDiagnosisDto updateDiagnosisDto);
        Task<List<Diagnosis>> SearchDiagnosisbyMRAsync(string id);
        Task<List<MedicalRecordWithPatientDto>> ListWaitingPatientAsync();
        Task<List<MedicalRecordWithPatientDto>> ListTreatedPatientAsync();
    }

    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;

        public DoctorService(
            AppDbContext context,
            IConfiguration configuration,
            IAuditService auditService
        )
        {
            _context = context;
            _configuration = configuration;
            _auditService = auditService;
        }

        public async Task<string> CreateDiagnosisAsync(CreateDiagnosisDto createDiagnosisDto)
        {
            var diagnosis = new Diagnosis
            {
                Id = Guid.NewGuid(), // Id vẫn là Guid cho EF Core
                DiagnosisId = $"CD{Guid.NewGuid():N}".Substring(0, 6).ToUpper(), // Id string dùng làm key thao tác
                MedicalRecordId = createDiagnosisDto.MedicalRecordId,
                DiagnosedDate = createDiagnosisDto.DiagnosedDate,
                Notes = createDiagnosisDto.Notes?.Trim(),
            };

            var medicalRecord = await _context.MedicalRecords.FirstOrDefaultAsync(r =>
                r.MedicalRecordId == createDiagnosisDto.MedicalRecordId
            );
            if (medicalRecord != null)
            {
                medicalRecord.Status = true;
            }

            await _context.Diagnoses.AddAsync(diagnosis);
            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Doctor",
                    Action = "Create Diagnosis",
                    Details = createDiagnosisDto,
                }
            );
            return diagnosis.DiagnosisId;
        }

        public async Task<Diagnosis> UpdateDiagnosisAsync(
            string diagnosisId,
            UpdateDiagnosisDto updateDiagnosisDto
        )
        {
            var diagnosis = await _context.Diagnoses.FirstOrDefaultAsync(d =>
                d.DiagnosisId == diagnosisId
            );

            if (diagnosis == null)
            {
                throw new Exception("Không tồn tại chẩn đoán với DiagnosisId này.");
            }

            // Cập nhật các trường nếu có dữ liệu mới
            if (!string.IsNullOrEmpty(updateDiagnosisDto.Notes))
                diagnosis.Notes = updateDiagnosisDto.Notes;

            await _context.SaveChangesAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Doctor",
                    Action = "Update Diagnosis",
                    Details = updateDiagnosisDto,
                }
            );
            return diagnosis;
        }

        public async Task<List<Diagnosis>> SearchDiagnosisbyMRAsync(string medicalRecordId)
        {
            var diagnoses = await _context
                .Diagnoses.Where(d => d.MedicalRecordId == medicalRecordId)
                .Include(d => d.MedicalRecord)
                .ToListAsync();

            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Doctor",
                    Action = "Get Diagnoses for MedicalRecordId",
                    Details = diagnoses,
                }
            );

            return diagnoses;
        }

        public async Task<List<MedicalRecordWithPatientDto>> ListWaitingPatientAsync()
        {
            var records = await _context.MedicalRecords.Where(r => r.Status == false).ToListAsync();
            var results = new List<MedicalRecordWithPatientDto>();

            foreach (var r in records)
            {
                var p = await _context.Patients.FirstOrDefaultAsync(p =>
                    p.IdPatient == r.PatientId
                );
                if (p != null)
                {
                    results.Add(
                        new MedicalRecordWithPatientDto
                        {
                            Id = r.Id.ToString(),
                            PatientId = r.PatientId,
                            MedicalRecordId = r.MedicalRecordId,
                            PhysicicanId = r.AssignedPhysicianId,
                            CreatedAt = r.CreatedDate,
                            FullName = p.FullName,
                            DateOfBirth = p.DateOfBirth,
                            Gender = p.Gender,
                            Phone = p.Phone,
                            Email = p.Email,
                            MedicalHistory = p.MedicalHistory,
                            Symptoms = r.Symptoms,
                            status = r.Status,
                        }
                    );
                }
            }

            return results;
        }

        public async Task<List<MedicalRecordWithPatientDto>> ListTreatedPatientAsync()
        {
            var records = await _context.MedicalRecords.Where(r => r.Status == true).ToListAsync();
            var results = new List<MedicalRecordWithPatientDto>();

            foreach (var r in records)
            {
                var p = await _context.Patients.FirstOrDefaultAsync(p =>
                    p.IdPatient == r.PatientId
                );
                if (p != null)
                {
                    var diagnosis = await _context.Diagnoses.FirstOrDefaultAsync(d =>
                        d.MedicalRecordId == r.MedicalRecordId
                    );
                    results.Add(
                        new MedicalRecordWithPatientDto
                        {
                            Id = r.Id.ToString(),
                            PatientId = r.PatientId,
                            FullName = p.FullName,
                            Gender = p.Gender,
                            Email = p.Email,
                            Phone = p.Phone,
                            DateOfBirth = p.DateOfBirth,
                            CreatedAt = r.CreatedDate,
                            MedicalRecordId = r.MedicalRecordId,
                            PhysicicanId = r.AssignedPhysicianId,
                            MedicalHistory = p.MedicalHistory,
                            Symptoms = r.Symptoms,
                            DiagnosisNotes = diagnosis?.Notes,
                            status = r.Status,
                        }
                    );
                }
            }

            return results;
        }
    }
}
