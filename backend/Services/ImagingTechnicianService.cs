using backend.DTOs;

public interface IImagingTechnicianService
{
    // Image Upload
    Task<ImageDto> UploadImageAsync(UploadImageDto uploadDto, Guid uploadedByUserId); // Handles saving file to storage & DB record

    // Image Management
    Task<ImageDto> UpdateImageStatusAsync(Guid imageId, string newStatus, Guid updatedByUserId); // E.g., for general internal status updates
    Task<ImageDto> AssignImageToMedicalRecordAsync(
        Guid imageId,
        Guid medicalRecordId,
        Guid updatedByUserId
    );
    Task<ImageDto> RetakeImageAsync(Guid imageId, string reason, Guid technicianId); // If a retake is needed for a bad image
}
