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
        Task<Diagnosis> UpdateDiagnosisAsync(Guid id, UpdateDiagnosisDto updateDiagnosisDto);
        Task<List<Diagnosis>> SearchDiagnosisbyMRAsync(Guid id);
        // Task<List<User>> ListDoctorAsync();
        // Task<string> CreateMediaRecordAsync(CreateMedicalRecordDto createMedicalRecordDto);
        // Task<List<MedicalRecord>> SearchlistMediaRecordbyId(Guid id);
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
                Id = Guid.NewGuid(),
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
            return diagnosis.Id.ToString();
        }

        public async Task<Diagnosis> UpdateDiagnosisAsync(
            Guid id,
            UpdateDiagnosisDto updateDiagnosisDto
        )
        {
            var diagnosis = await _context.Diagnoses.FindAsync(id);
            if (diagnosis == null)
            {
                throw new Exception("Không tồn tại chẩn đoán với ID này.");
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

        public async Task<List<Diagnosis>> SearchDiagnosisbyMRAsync(Guid id)
        {
            var diagnosis = await _context
                .Diagnoses.Where(d => d.MedicalRecordId == id)
                .Include(d => d.MedicalRecord) // <--- Add this line to eagerly load MedicalRecord
                .ToListAsync();
            await _auditService.WriteLogAsync(
                new WriteLogDto
                {
                    User = "Doctor",
                    Action = "Get Diagnoses for mrId",
                    Details = diagnosis,
                }
            );

            return diagnosis;
        }
    }
}
