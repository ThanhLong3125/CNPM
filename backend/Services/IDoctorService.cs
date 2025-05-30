using backend.DTOs;

namespace backend.Services;

public interface IDoctorService
{
    // View Medical Records and related data
    Task<MedicalRecordDto?> GetMedicalRecordForDoctorAsync(Guid medicalRecordId, Guid doctorId);
    Task<IEnumerable<MedicalRecordDto>> GetAssignedMedicalRecordsAsync(Guid doctorId);
    Task<IEnumerable<ImageDto>> GetImagesForMedicalRecordAsync(Guid medicalRecordId);
    Task<IEnumerable<ImageDto>> GetPatientImagesForComparisonAsync(Guid patientId); // For "Compare images over time"

    // Diagnosis and Reporting
    Task<MedicalRecordDto> SubmitDiagnosisAsync(
        Guid medicalRecordId,
        SubmitDiagnosisDto diagnosisDto,
        Guid doctorId
    );
    Task<MedicalRecordDto> AddOrUpdatePhysicianNotesAsync(
        Guid medicalRecordId,
        string notes,
        Guid doctorId
    ); // For general notes on the record

    // Image Interaction (Doctor specific)
    Task<ImageDto> AddOrUpdateImageNotesAsync(Guid imageId, string notes, Guid doctorId); // "Ghi chú và chú thích ảnh" (image-specific)
    Task<ImageDto> AnalyzeImageWithAIAsync(Guid imageId, Guid doctorId); // Trigger AI analysis and store results
}
