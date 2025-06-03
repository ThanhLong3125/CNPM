using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IDoctorService
{
    
    Task<List<PatientDto>> GetPatientsByCreatedDateAsync(DateTime? startDate, DateTime? endDate);
    Task<List<ImageDICOMDto>> GetDicomImagesByPatientIdAsync(int patientId);
    Task<List<MedicalRecordsDto>> GetPriorityPatientsAsync();
    Task<bool> AddDoctorCommentAsync(Guid imageId, string comment);
}


     

    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }
        // truy cập csdl 

       public async Task<List<PatientDto>> GetPatientsByCreatedDateAsync(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.MedicalRecords
            .Include(m => m.Patient)
            .AsQueryable();

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(m => m.CreatedDate >= startDate && m.CreatedDate <= endDate);
        }

        var records = await query.ToListAsync();

        return records.Select(m => new PatientDto
        {
            Id = m.Patient.Patient_ID,
            FullName = m.Patient.Full_name,
            DateOfBirth = m.Patient.DateOfBirth,
            Gender = m.Patient.Gender,
            Email = m.Patient.ContactInfo  // hoặc email nếu có
        })
    // Loại bỏ bệnh nhân trùng nếu có nhiều hồ sơ
        .GroupBy(p => p.Id)
        .Select(g => g.First())
        .ToList();
    }

// xem hồ sơ ưu tiên 
        public async Task<List<MedicalRecordsDto>> GetPriorityPatientsAsync()
    {
        var priorityRecords = await _context.MedicalRecords
            .Include(m => m.Patient)
            .Where(m => m.IsPriority == true)
            .ToListAsync();

        return priorityRecords.Select(m => new MedicalRecordsDto
        {
            RecordId = m.Record_ID,
            FullName = m.Patient.Full_name,
            DateOfBirth = m.Patient.DateOfBirth,
            Gender = m.Patient.Gender,
            ContactInfo = m.Patient.ContactInfo,
            CreatedDate = m.CreatedDate,
            Symptoms = m.Symptoms,
            Status = m.Status
        }).ToList();
    }


        public async Task<List<ImageDICOMDto>> GetDicomImagesByPatientIdAsync(int patientId)
    {
        var images = await _context.ImageDICOM
            .Where(img => img.PatientID == patientId)
            .ToListAsync();

        return images.Select(img => new ImageDICOMDto
        {
            Id = img.Image_ID,
            FilePath = img.File_Path,
            AcquisitionDateTime = img.Acquisition_DateTime,
            ImageType = img.ImageType ?? string.Empty
        }).ToList();
    }


        public async Task<bool> AddDoctorCommentAsync(Guid imageId, string comment)
        {
            var image = await _context.ImageDICOM.FindAsync(imageId);
            if (image == null)
                throw new ApplicationException("Image not found");

            image.DoctorComment = comment;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
