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
                Id = Guid.NewGuid(),  // Id vẫn là Guid cho EF Core
                DiagnosisId = $"CD{Guid.NewGuid():N}".Substring(0, 6).ToUpper(),  // Id string dùng làm key thao tác
                MedicalRecordId = createDiagnosisDto.MedicalRecordId,
                DiagnosedDate = createDiagnosisDto.DiagnosedDate,
                Notes = createDiagnosisDto.Notes?.Trim(),
            };

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

        public async Task<Diagnosis> UpdateDiagnosisAsync(string diagnosisId, UpdateDiagnosisDto updateDiagnosisDto)
        {
            var diagnosis = await _context.Diagnoses
                .FirstOrDefaultAsync(d => d.DiagnosisId == diagnosisId);

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
                .Diagnoses
                .Where(d => d.MedicalRecordId == medicalRecordId)
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
    }
}
